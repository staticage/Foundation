<template>
    <section>
        <el-row class="button-top-row" v-if="$hasPermission('新增')">
            <el-button-group>
                <el-button type="primary" size="mini" @click="create" icon="el-icon-circle-plus">新增</el-button>
            </el-button-group>
        </el-row>
        <el-row class="search-row el-form el-form--inline">
            <el-col :span="24">
                <label>公寓名称：</label>
                <el-select v-model="query.apartmentId" placeholder="请选择公寓名称">
                    <el-option label="全部" value></el-option>
                    <el-option v-for="item in apartments" :key="item.id" :label="item.name"
                               :value="item.id"></el-option>
                </el-select>
                <label>费用名称：</label>
                <el-input v-model="query.name" placeholder="费用名称"></el-input>
                <el-button type="primary" icon="el-icon-search" @click="search()">查询</el-button>
                <!-- <el-button type="info" plain @click="query={}">重置</el-button> -->
            </el-col>
        </el-row>
        <el-row class="table-row">
            <el-table :data="resource.items">
                <el-table-column prop="name" label="费用名称"></el-table-column>
                <el-table-column prop="apartmentName" label="公寓名称"></el-table-column>
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
                        <el-button @click="details(scope.row)" type="text">查看</el-button>
                        <el-button
                                @click="enabled(scope.row)"
                                type="text"
                        >{{scope.row.enabled != true ? '启用' : '禁用'}}
                        </el-button>
                        <el-button @click="edit(scope.row)" type="text"
                                   v-if="scope.row.canEdit && $hasPermission('编辑')">编辑
                        </el-button>
                        <el-button @click="clone(scope.row)" type="text">克隆</el-button>
                        <!--<el-button @click="del(scope.row)" type="text">删除</el-button>-->
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
                title="房型信息"
                :visible.sync="dialogFormVisible"
                width="30%"
        >
            <el-form ref="form" :rules="rules" :model="command" label-width="80px">
                <el-form-item label="公寓" prop="apartmentId">
                    <el-select v-model="command.apartmentId" style="width: 100%;" placeholder="请选择公寓">
                        <el-option
                                v-for="item in apartments"
                                :key="item.id"
                                :label="item.name"
                                :value="item.id"
                        ></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="价格" prop="amount">
                    <el-input-number
                            v-model="command.amount"
                            controls-position="right"
                            :min="0"
                            :precision="2"
                            style="width:100%;"
                            label="请输入价格"
                    ></el-input-number>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="back">取 消</el-button>
            </div>
        </el-dialog>
    </section>
</template>
<script lang="ts">
    import ElementUI from "element-ui";
    import api from "../../../../config/api";
    // import _ from "lodash";

    export default {
        components: {},

        data() {
            return {
                command: {},
                query: {},
                dialogFormVisible: false,
                apartments: [],
                loading: false,
                resource: [],
                rules: {
                    amount: [{required: true, message: "请输入价格", trigger: "blur"}],
                    apartmentId: [
                        {required: true, message: "请选择公寓", trigger: "change"}
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
                    "apartment/price-package/query",
                    this.query
                )).data;
            },

            create() {
                this.$router.push({
                    name: "price-package-create"
                });
            },

            edit(row) {
                this.$router.push({
                    name: "price-package-edit",
                    params: {id: row.id}
                });
            },

            details(row) {
                this.$router.push({
                    name: "price-package-edit",
                    params: {id: row.id, disabled: true}
                });
            },

            clone(row) {
                this.$router.push({
                    name: "price-package-edit",
                    params: {id: row.id, clone: true}
                });
            },

            enabled(row) {
                let text = row.enabled != true ? "启用" : "禁用";
                this.$confirm("是否确认【" + text + "】?", "提示", {
                    confirmButtonText: "确定",
                    cancelButtonText: "取消",
                    type: "warning"
                })
                    .then(() => {
                        this.$axios
                            .post("price-package/change-status", {
                                ids: [row.id],
                                enabled: !row.enabled
                            })
                            .then(
                                response => {
                                    this.loading = false;
                                    this.$message({
                                        type: "success",
                                        message: "操作成功!"
                                    });
                                    this.search();
                                },
                                err => {
                                    this.loading = false;
                                    this.$message({
                                        type: "error",
                                        message: "操作失败:" + err.message,
                                        center: true
                                    });
                                }
                            );
                    })
                    .catch(() => {
                        // catch 不要删除，Uncaught (in promise) cancel
                    });
            },

            async del(row) {
                this.$confirm("确认删除?", "提示", {
                    confirmButtonText: "确定",
                    cancelButtonText: "取消",
                    type: "warning"
                })
                    .then(() => {
                        this.$axios.delete("service-cost/" + row.id).then(
                            response => {
                                this.loading = false;
                                this.$message({
                                    type: "success",
                                    message: "删除成功!"
                                });
                                this.search();
                            },
                            response => {
                                this.loading = false;
                                const result = response.response.data;
                                this.$message({
                                    type: "error",
                                    message: "删除失败:" + result.error,
                                    center: true
                                });
                            }
                        );
                    })
                    .catch(() => {
                        // catch 不要删除，Uncaught (in promise) cancel
                    });
            },

            back() {
                this.dialogFormVisible = false;
                this.command = {};
                this.$refs.form.resetFields();
            }
        }
    };
</script>
