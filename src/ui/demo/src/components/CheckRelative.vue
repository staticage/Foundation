// 客户亲属信息
<template>
    <el-row>
        <el-table :data="resource.items">
            <!--<el-table-column prop="avatar" label="头像"></el-table-column>-->
            <el-table-column prop="name" label="姓名"></el-table-column>
            <el-table-column prop="sexText" label="性别"></el-table-column>
            <el-table-column prop="asTypesText" label="关系"></el-table-column>
            <el-table-column prop="idCardNo" label="证件号"></el-table-column>
            <el-table-column prop="phone" label="手机号码"></el-table-column>
            <el-table-column prop="tel" label="固定电话"></el-table-column>
            <el-table-column prop="workUnit" label="工作单位"></el-table-column>
            <el-table-column prop="email" label="电子邮箱"></el-table-column>
            <el-table-column prop="address" label="地址"></el-table-column>
            <el-table-column fixed="right" label="操作">
                <template slot-scope="scope">
                    <el-button @click="details(scope.row)" type="text">上传头像</el-button>
                    <el-button @click="edit(scope.row)" type="text" v-if="$hasPermission('编辑')">编辑</el-button>
                    <el-button @click="del(scope.row)" type="text" v-if="$hasPermission('删除')">删除</el-button>
                </template>
            </el-table-column>
        </el-table>
        <!-- <el-pagination
            :pager-count="4"
            :current-page="currentPage"
            :page-sizes="[10, 20, 30, 40]"
            :page-size="100"
            layout="total,prev,pager,next,sizes,jumper"
            :total="400"
        ></el-pagination>-->
        <!-- @size-change="handleSizeChange"
        @current-change="handleCurrentChange"-->
    </el-row>
</template>

<script>
    import api from "../config/api";

    export default {
        props: {
            id: {
                type: String,
                default: function () {
                    return "";
                }
            },
            refresh: {
                type: Boolean,
                default: function () {
                    return false;
                }
            }
        },

        data() {
            return {
                dialogForm: {},
                dialogFormVisible: false,
                query: {pageSize: 20000},
                resource: {}
            };
        },

        mounted() {
            this.$watch("dialog", newValue => {
                this.dialogFormVisible = newValue;
                if (this.dialogFormVisible) {
                    this.create();
                }
            });
            this.$watch("id", newValue => {
                // alert(this.$props.id + "--------" + newValue);
                this.init();
            });
            this.$watch("refresh", newValue => {
                if (newValue == true) {
                    this.search();
                }
            });
        },

        methods: {
            async search() {
                this.resource = (await this.$axios.post(
                    api.customerFamily + "/query",
                    this.query
                )).data;
            },
            init() {
                this.query.customerId = this.$props.id;
                this.search();
            },

            del(row) {
                this.$confirm("确认删除?", "提示", {
                    confirmButtonText: "确定",
                    cancelButtonText: "取消",
                    type: "warning"
                })
                    .then(() => {
                        this.$axios.delete(api.customerFamily + "/" + row.id).then(
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
                // 向子组件转递方法
                this.$emit("from", {
                    dialog: this.dialogFormVisible,
                    data: row,
                    status: 'delete'
                });
            },
            edit(row) {
                this.dialogFormVisible = true;
                // 向子组件转递方法
                this.$emit("from", {
                    dialog: this.dialogFormVisible,
                    data: row,
                    status: 'edit'
                });
            },
            create() {
                // this.dialogFormVisible = true;
                // this.$router.push({
                //     name: "apartment-create",
                //     params: {
                //         id: "id123"
                //     }
                // });
            },
            onClick() {
                // this.isMore = !this.isMore;
                // // 根据v-model对封装的组件赋值，必须要填写
                // this.$emit("input", this.isMore);
            }
        }
    };
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
    @import "../element-variables";

    .moreBtn {
        display: table;
        white-space: nowrap;
        border-spacing: 0px 0;
        font-size: 12px;
        color: $--color-primary;
        // margin-bottom: 18px;
    }

    .moreBtn:before,
    .moreBtn:after {
        display: table-cell;
        content: "";
        width: 50%;
        background: -webkit-linear-gradient(#eee, #eee) repeat-x left center;
        background: linear-gradient(#eee, #eee) repeat-x left center;
        background-size: 1px 1px;
    }

    .moreBtn sapn {
        cursor: pointer;
    }
</style>