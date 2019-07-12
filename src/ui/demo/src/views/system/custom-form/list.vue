<template>
    <section>
        <el-row class="button-top-row">
            {{customForm}}
            {{resource}}
            <el-button-group>
                <el-button type="primary" size="mini" @click="create" icon="el-icon-circle-plus">新增</el-button>
            </el-button-group>
        </el-row>

        <el-row class="table-row">
            <el-table :data="resource">
                <el-table-column v-for="field in tableFields" :key="field.name" :prop="field.name" :label="field.label"></el-table-column>
            </el-table>
            <el-pagination :current-page="query.page" :page-sizes="[10, 20, 30, 40]" :page-size="query.pageSize" layout="total,prev,pager,next,sizes,jumper" :total="resource.total" @size-change="handleSizeChange" @current-change="handleCurrentChange"></el-pagination>
        </el-row>
        <!-- <el-dialog :close-on-click-modal="false" title="机构信息" :visible.sync="dialogFormVisible" width="30%">
            <el-form ref="dialogForm" :rules="rules" :model="dialogForm" label-width="80px">
                <el-form-item label="机构名称" prop="name">
                    <el-input v-model="dialogForm.name" placeholder="请输入机构名称"></el-input>
                </el-form-item>
                <el-form-item label="机构法人" prop="corporation">
                    <el-input v-model="dialogForm.corporation" placeholder="请输入机构法人"></el-input>
                </el-form-item>
                <el-form-item label="联系电话" prop="phone">
                    <el-input v-model="dialogForm.phone" placeholder="请输入联系电话"></el-input>
                </el-form-item>
                <el-form-item label="电子邮箱" prop="email">
                    <el-input v-model="dialogForm.email" placeholder="请输入电子邮箱"></el-input>
                </el-form-item>
                <el-form-item label="账户名称" prop="accountName">
                    <el-input v-model="dialogForm.accountName" placeholder="请输入账户名称"></el-input>
                </el-form-item>
                <el-form-item label="账户号码" prop="accountNumber">
                    <el-input v-model="dialogForm.accountNumber" placeholder="请输入账户号码"></el-input>
                </el-form-item>
                <el-form-item label="开户行" prop="bankName">
                    <el-input v-model="dialogForm.bankName" placeholder="请输入开户行"></el-input>
                </el-form-item>
                <el-form-item label="邮政编码" prop="postcode">
                    <el-input v-model="dialogForm.postcode" placeholder="请输入邮政编码"></el-input>
                </el-form-item>
                <el-form-item label="机构地址" prop="address">
                    <el-input v-model="dialogForm.address" placeholder="请输入机构地址"></el-input>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="cancel">取 消</el-button>
                <el-button type="primary" @click="save">确 定</el-button>
            </div>
        </el-dialog>-->
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
    computed: {
        tableFields() {
            return this.fields.filter(x => x.isShowInTable)
        }
    },
    data() {
        return {
            customForm: {},
            query: {},
            resource: [],
            dialogFormVisible: false,
            loading: false,
            user: {},
            fields: []
        };
    },

    async mounted() {
        this.customForm = (await this.$axios.get(`custom-form/${this.$route.params.id}`)).data
        this.fields = this.customForm.fieldGroups
            .map(x => x.fields)
            .reduce((x, y) => x.concat(y), [])
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
        async search() {
            this.resource = (await this.$axios.post(`entity/${this.customForm.name}/_query`, {})).data;
            console.log(this.resource);
        },
        create() {
            this.dialogForm = { id: "" };
            this.dialogFormVisible = true;
            if (this.$refs["dialogForm"]) {
                this.$refs["dialogForm"].clearValidate();
            }
        },
        async remove(id) {
            this.$axi
            this.$confirm("确认删除?", "提示", {
                confirmButtonText: "确定",
                cancelButtonText: "取消",
                type: "warning"
            })
                .then(() => {
                    this.loading = true;
                    this.$axios.delete(api.company + "/" + id).then(
                        response => {
                            this.loading = false;
                            this.$message({
                                type: "success",
                                message: "删除成功!"
                            });
                            this.search();
                        },
                        err => {
                            this.loading = false;
                            const result = err.response.data;
                            this.$message({
                                type: "error",
                                message: "删除失败:" + result.Message,
                                center: true
                            });
                        }
                    );
                })
                .catch(() => {
                    // catch 不要删除，Uncaught (in promise) cancel
                });
        },
        save() {
            this.$refs.dialogForm.validate(valid => {
                if (valid) {
                    this.loading = true;
                    const httpRequest = this.dialogForm.id
                        ? this.$axios.put(api.company, this.dialogForm)
                        : this.$axios.post(api.company, this.dialogForm);
                    httpRequest.then(
                        response => {
                            this.loading = false;
                            this.dialogFormVisible = false;
                            this.$message({
                                message: "保存成功",
                                type: "success"
                            });
                            this.search();
                        },
                        error => {
                            this.loading = false;
                            const result = error.response.data;
                            this.$message({
                                type: "error",
                                message: "保存失败:" + result.Message
                            });
                        }
                    );
                } else {
                    return;
                }
            });
        },

        async editShow(id) {
            this.dialogForm = (await this.$axios.get(api.company + "/" + id)).data;
            this.dialogFormVisible = true;
            this.$refs["dialogForm"].clearValidate();
        },

        cancel() {
            this.dialogFormVisible = false;
            this.$refs["dialogForm"].clearValidate(); // 重置验证
        }
    }
};
</script>
