<template>
    <section>
        <el-row class="button-top-row" v-if="$hasPermission('新增')">
            <el-button-group>
                <el-button type="primary" size="mini" @click="create" icon="el-icon-circle-plus">新增</el-button>
            </el-button-group>
        </el-row>
        <el-row class="search-row el-form el-form--inline">
            <el-col :span="24">
                <label>角色名称：</label>
                <el-input v-model="query.name" placeholder="角色名称"></el-input>
                <el-button type="primary" icon="el-icon-search" @click="search()">查询</el-button>
                <el-button type="info" plain @click="reset()">重置</el-button>
            </el-col>
        </el-row>
        <el-row class="table-row">
            <el-table :data="resource.items">
                <el-table-column prop="name" label="名称"></el-table-column>
                <el-table-column prop="description" label="描述"></el-table-column>
                <el-table-column fixed="right" label="操作">
                    <template slot-scope="scope">
                        <el-button @click="perm(scope.row)" type="text">权限</el-button>
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
        </el-row>
        <el-dialog
                :close-on-click-modal="false"
                v-loading="loading"
                title="角色信息"
                :visible.sync="dialogFormVisible"
                width="30%"
        >
            <el-form ref="form" :rules="rules" :model="command" label-width="80px">
                <el-form-item label="角色名称" prop="name">
                    <el-input placeholder="请输入角色名称" v-model="command.name"></el-input>
                </el-form-item>
                <el-form-item label="角色描述" prop="description">
                    <el-input placeholder="请输入角色描述" v-model="command.description"></el-input>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="back">取 消</el-button>
                <el-button type="primary" @click="save">确 定</el-button>
            </div>
        </el-dialog>
        <Permission
                :id="command.id"
                :visible="dialogPermissionVisible"
                @callback="permissionVisible"
                :data.sync="command.permissions"
        ></Permission>
    </section>
</template>
<script lang="ts">
    import _ from "lodash";
    import Permission from "../../../components/Permission.vue";
    import ElementUI, {Tree} from "element-ui";

    export default {
        components: {
            Permission
        },

        data() {
            return {
                command: {},
                dialogFormVisible: false,
                dialogPermissionVisible: false,
                resource: [],
                loading: false,
                rules: {
                    name: [{required: true, message: "请输入角色名称", trigger: "blur"}]
                },
                query: {
                    page: 1,
                    pageSize: 10
                }
            };
        },

        mounted() {
            // this.query.page = 1;
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

            reset() {
                this.query = {
                    page: 1,
                    pageSize: 10,
                    name: ""
                };
                // this.search();
            },

            async search() {
                this.resource = (await this.$axios.post("role/query", this.query)).data;
            },

            create() {
                this.dialogFormVisible = true;
                this.command = {};
                if (this.$refs["form"]) {
                    this.$refs["form"].clearValidate(); // 重置验证
                }
            },

            edit(row) {
                this.dialogFormVisible = true;
                this.command = _.cloneDeep(row);
                if (this.$refs["form"]) {
                    this.$refs["form"].clearValidate(); // 重置验证
                }
            },

            perm(row) {
                this.command = {
                    id: row.id,
                    name: row.name,
                    description: row.description,
                    permissions: row.permissions
                };
                this.dialogPermissionVisible = true;
            },

            // 接收父组件传过来的参数
            permissionVisible(e) {
                this.dialogPermissionVisible = e.visible;
                if (e.status === true) {
                    this.search();
                }
            },

            del(row) {
                this.$confirm("确认删除?", "提示", {
                    confirmButtonText: "确定",
                    cancelButtonText: "取消",
                    type: "warning"
                })
                    .then(() => {
                        this.$axios.delete("role/" + row.id).then(
                            response => {
                                this.loading = false;
                                this.$message({
                                    type: "success",
                                    message: "删除成功!"
                                });
                                this.search();
                            }
                        ).catch(err => {
                            this.saving = false;
                            this.$message({
                                message: "提交失败: " + err.message,
                                type: "error"
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
                            ? this.$axios.put("role/edit", this.command)
                            : this.$axios.post("role/create", this.command);
                        httpRequest.then(
                            response => {
                                this.loading = false;
                                this.$message({
                                    type: "success",
                                    message: "保存成功",
                                    center: true
                                });
                                this.search();
                                this.back();
                            },
                            response => {
                                this.loading = false;
                                const result = response.response.data;
                                this.$message({
                                    type: "error",
                                    message: "保存失败:" + result.Message,
                                    showClose: true,
                                    duration: 0
                                });
                            }
                        );
                    } else {
                        return false;
                    }
                });
            },

            back() {
                this.dialogFormVisible = false;
                this.command = {};
            }
        }
    };
</script>
