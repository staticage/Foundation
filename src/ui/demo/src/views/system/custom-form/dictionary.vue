<template>
    <section>
        <el-row class="button-top-row">
            <el-button-group>
                <el-button type="primary" size="mini" @click="create" icon="el-icon-circle-plus">新增</el-button>
            </el-button-group>
        </el-row>
        <el-row class="search-row el-form el-form--inline">
            <el-col :span="24">
                <label>机构名称：</label>
                <el-input v-model="query.name" placeholder="机构名称"></el-input>
                <el-button type="primary" icon="el-icon-search" @click="search">查询</el-button>
            </el-col>
        </el-row>
        <el-row class="table-row">
            <el-table :data="resource" v-loading="searching">
                <el-table-column prop="name" label="名称" width="200"></el-table-column>
                <el-table-column label="配置">
                    <template slot-scope="scope">
                        <el-tag style="margin-right:5px" v-for="(item, index) in scope.row.items.filter(x=> x.enabled)" :key="index" type="info" effect="plain">{{ item.label }}</el-tag>
                    </template>
                </el-table-column>
                <el-table-column fixed="right" label="操作" width="80" align="right">
                    <template slot-scope="scope">
                        <el-button @click="edit(scope.row)" type="text" v-if="$hasPermission('编辑')">编辑</el-button>
                    </template>
                </el-table-column>
            </el-table>
            <el-pagination :current-page="query.page" :page-sizes="[10, 20, 30, 40]" :page-size="query.pageSize" layout="total,prev,pager,next,sizes,jumper" :total="resource.total" @size-change="handleSizeChange" @current-change="handleCurrentChange"></el-pagination>
        </el-row>
        <el-dialog :close-on-click-modal="false" title="数据字典" :visible.sync="isEditing" width="50%">
            <el-form ref="form" :rules="rules" :model="setting" label-width="100px">
                <el-form-item label="数据名称" prop="name" :rules="{required: true, message: '数据名称不能为空', trigger: 'blur'}">
                    <el-input v-model="setting.name" placeholder="请输入数据名称"></el-input>
                </el-form-item>
                <el-row style="margin-bottom:10px;">
                    <el-button @click="addItem" style="float:right">添加项</el-button>
                </el-row>

                <el-row v-for="(item, index) in setting.items" :key="index">
                    <el-col :span="2">
                        <el-form-item :label="item.sortNumber + '.'"></el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="数据项" :prop="'items.' + index + '.label'" :rules="{required: true, message: '数据项不能为空', trigger: 'blur'}">
                            <el-input v-model="item.label" placeholder="请输入数据项"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="是否启用" :prop="'items.' + index + '.enabled'">
                            <el-switch v-model="item.enabled"></el-switch>
                            <el-button v-if="item.isNew" style="margin-left:15px;" type="danger" size="mini" @click="removeItem(index)">删除</el-button>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8"></el-col>
                </el-row>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="isEditing = false">取 消</el-button>
                <el-button type="primary" @click="save">保 存</el-button>
            </div>
        </el-dialog>
    </section>
</template>
<script >
import api from "../../../config/api";
import validators from "../../../common/validators";

export default {
    components: {},
    props: {
        customFormId: {
            type: Number
        }
    },
    data() {
        return {
            customForm: {},
            query: {},
            resource: [],
            isEditing: false,
            loading: false,
            user: {},
            rules: {},
            setting: {},
            searching: false
        };
    },

    async mounted() {
        this.search();
    },
    methods: {
        handleSizeChange(pageSize) {
            this.query.pageSize = pageSize;
            this.search();
        },
        handleCurrentChange(page) {
            this.query.page = page;
            this.search();
        },

        addItem() {
            this.setting.items.push({ enabled: true, label: "", isNew: true })
            this.refreshSortNumber()
        },
        removeItem(index) {
            this.setting.items.splice(index, 1)
            this.refreshSortNumber()
        },

        async search() {
            this.searching = true;
            try {
                this.resource = (await this.$axios.post("setting/_query", this.query)).data
            } catch (error) {

            }
            this.searching = false;
        },
        create() {
            this.setting = {
                name: "",
                items: []
            }
            this.isEditing = true;
            this.$refs["form"] && this.$refs["form"].clearValidate()
        },
        refreshSortNumber() {
            this.setting.items.forEach((item, index) => item.sortNumber = index + 1)
        },
        async save() {
            const valid = await this.$refs.form.validate();
            if (valid) {
                try {
                    this.loading = true;
                    this.setting.id
                        ? await this.$axios.put("setting", this.setting)
                        : await this.$axios.post("setting", this.setting);
                    this.isEditing = false;
                    this.$message({
                        message: "保存成功",
                        type: "success"
                    });
                    this.search();
                } catch (error) {

                    const result = error.response.data;
                    this.$message({
                        type: "error",
                        message: "保存失败:" + result.Message
                    });
                }
                this.loading = false;
            }
        },

        async edit(setting) {
            this.setting = JSON.parse(JSON.stringify(setting))
            this.isEditing = true;
            this.$refs["form"] && this.$refs["form"].clearValidate()
        },

        cancel() {
            this.isEditing = false;
            this.$refs["form"] && this.$refs["form"].clearValidate(); // 重置验证
        }
    }
};
</script>
