<template>
    <section>
        <el-row class="button-top-row" v-if="$hasPermission('新增')">
            <el-button-group>
                <el-button type="primary" size="mini" @click="create" icon="el-icon-circle-plus">新增</el-button>
            </el-button-group>
        </el-row>
        <el-row class="search-row el-form el-form--inline">
            <el-col :span="24">
                <label>所属公寓：</label>
                <el-select v-model="query.apartmentId" placeholder="请选择所属公寓">
                    <el-option v-for="item in apartments" :key="item.id" :label="item.name"
                               :value="item.id"></el-option>
                </el-select>
                <label>耗材名称：</label>
                <el-input v-model="query.name" placeholder="请输入耗材名称"></el-input>
                <el-button type="primary" icon="el-icon-search" @click="search">查询</el-button>
            </el-col>
        </el-row>
        <el-row class="table-row">
            <el-table :data="resource.items">
                <el-table-column prop="apartmentName" label="所属公寓"></el-table-column>
                <!-- <el-table-column prop="code" label="服务编码"></el-table-column> -->
                <el-table-column prop="name" label="耗材名称"></el-table-column>
                <el-table-column prop="price" label="单次价格"></el-table-column>
                <el-table-column prop="enabled" label="状态">
                    <template slot-scope="scope">
                        <el-tag
                                :type="scope.row.enabled === true ? 'success' : 'danger'"
                                disable-transitions
                        >{{scope.row.enabled === true ? '启用' : '禁用'}}
                        </el-tag>
                    </template>
                </el-table-column>
                <el-table-column fixed="right" label="操作">
                    <template slot-scope="scope">
                        <el-button @click="edit(scope.row)" type="text" v-if="$hasPermission('编辑')">编辑</el-button>
                        <el-button @click="del(scope.row)" type="text" v-if="$hasPermission('删除')">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
            <el-pagination
                    :current-page="query.page"
                    :page-sizes="[10, 20, 30, 40]"
                    :page-size="query.pageSize"
                    layout="total,prev,pager,next,sizes,jumper"
                    :total="resource.total"
                    @size-change="handleSizeChange"
                    @current-change="handleCurrentChange"
            ></el-pagination>
            <!-- @size-change="handleSizeChange"
            @current-change="handleCurrentChange"-->
        </el-row>
        <el-dialog
                :close-on-click-modal="false"
                title="服务耗材"
                :visible.sync="dialogFormVisible"
                width="30%"
        >
            <el-form ref="form" :rules="rules" :model="command" label-width="80px">
                <el-form-item label="所属公寓" prop="apartmentId">
                    <el-select
                            v-model="command.apartmentId"
                            :disabled="isEdit"
                            style="width: 100%;"
                            placeholder="请选择所属公寓"
                    >
                        <el-option
                                v-for="item in apartments"
                                :key="item.id"
                                :label="item.name"
                                :value="item.id"
                        ></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="服务名称" prop="name">
                    <el-input v-model="command.name" placeholder="请输入服务名称"></el-input>
                </el-form-item>
                <el-form-item label="单次价格" prop="price">
                    <el-input-number
                            style="width: 100%;"
                            v-model="command.price"
                            controls-position="right"
                            :min="1"
                            :precision="2"
                            label="请输入单次价格"
                    ></el-input-number>
                </el-form-item>
                <el-form-item label="是否启用">
                    <el-switch v-model="command.enabled"></el-switch>
                </el-form-item>

            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="back">取 消</el-button>
                <el-button type="primary" @click="save">确 定</el-button>
            </div>
        </el-dialog>
    </section>
</template>
<script>
    import ElementUI from "element-ui";
    import _ from "lodash";
    import api from "../../../config/api.js";

    export default {
        components: {},
        data() {
            return {
                command: {},
                query: {page: 1},
                resource: {},
                dialogFormVisible: false,
                loading: false,
                apartments: [],
                isEdit: false,
                rules: {
                    apartmentId: [
                        {required: true, message: "请选择所属公寓", trigger: "change"}
                    ],
                    name: [{required: true, message: "请输入服务名称", trigger: "blur"}],
                    type: [{required: true, message: "请选择类型", trigger: "change"}],
                    price: [
                        {required: true, message: "请输入单次价格", trigger: "change"}
                    ]
                }
            };
        },

        async mounted() {
            this.apartments = (await this.$axios.get("apartment/select-list")).data;
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
                this.resource = (await this.$axios.post(
                    api.consumable + "/query",
                    this.query
                )).data;
            },

            create() {
                this.command = {id: "", enabled: true};
                this.dialogFormVisible = true;
                this.isEdit = false;
            },

            async del(row) {
                this.$confirm("确认删除?", "提示", {
                    confirmButtonText: "确定",
                    cancelButtonText: "取消",
                    type: "warning"
                })
                    .then(() => {
                        this.$axios.delete(api.consumable + "/" + row.id).then(
                            response => {
                                this.loading = false;
                                this.$message({
                                    type: "success",
                                    message: "删除成功!"
                                });
                                this.search();
                            }
                        ).catch(err => {
                            this.loading = false;
                            this.$message({
                                type: "error",
                                message: "删除失败:" + err.message,
                                center: true
                            });
                        });
                    })
                    .catch(() => {
                        // catch 不要删除，Uncaught (in promise) cancel
                    });
            },
            save() {
                this.$refs.form.validate(valid => {
                    if (valid) {
                        this.loading = true;
                        const httpRequest = this.command.id
                            ? this.$axios.put(api.consumable, this.command)
                            : this.$axios.post(api.consumable, this.command);
                        httpRequest.then(
                            response => {
                                this.loading = false;
                                this.$message({
                                    message: "保存成功",
                                    type: "success"
                                });
                                this.dialogFormVisible = false;
                                this.search();
                                this.back();
                            }
                        ).catch(err => {
                            this.loading = false;
                            this.$message({
                                type: "error",
                                message: "保存失败:" + err.message
                            });
                        });
                    } else {
                        return false;
                    }
                });
            },
            async edit(row) {
                this.command = _.clone(row);
                this.dialogFormVisible = true;
                this.isEdit = true;
            },

            back() {
                this.dialogFormVisible = false;
                this.command = {roleIds: []};
                if (this.$refs.form) {
                    this.$refs.form.resetFields();
                }
            }
        }
    };
</script>
