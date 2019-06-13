<template>
    <section>
        <el-row class="button-top-row">
            <el-button-group>
                <el-button type="primary" size="mini" @click="create" icon="el-icon-circle-plus">新建</el-button>
            </el-button-group>
        </el-row>
        <el-row class="search-row el-form el-form--inline">
            <el-col :span="24">
                <label>名称：</label>
                <el-input v-model="filters.numberOrName" placeholder="关键字"></el-input>
                <el-button type="primary" @click="search" :loading="searchLoading" icon="el-icon-search">查询</el-button>
                <el-button type="info" plain @click="filters={}">重置</el-button>
            </el-col>
        </el-row>
        <el-row class="table-row">
            <el-table :data="workflows">
                <el-table-column prop="name" label="工作流名称"></el-table-column>
                <el-table-column prop="type" label="类型"></el-table-column>
                <el-table-column prop="createdOn" label="创建日期"></el-table-column>
                <el-table-column label="操作">
                    <template slot-scope="scope">
                        <el-button @click="show(scope.row.id)" type="text">查看</el-button>
                        <el-button @click="edit(scope.row)" type="text" v-if="$hasPermission('编辑')">编辑</el-button>
                        <el-button @click="remove(scope.row.id,scope.$index)" type="text" v-if="$hasPermission('删除')">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
            <!--<el-pagination :current-page="currentPage" :page-sizes="[10, 20, 30, 40]" :page-size="100"-->
                           <!--layout="total,prev,pager,next,sizes,jumper" :total="total"></el-pagination>-->
        </el-row>


    </section>
</template>
<script>
    import api from "../../../config/api";
    import moment from 'moment';

    export default {
        components: {},
        data() {
            return {
                apartmentList: null,
                searchLoading: false,
                filters: {},
                workflows: [],
                total: 0,
                currentPage: 1,
                loading: false
            };
        },

        mounted() {
            this.$axios
                .get(api.workflow)
                .then(response => {
                    this.workflows = response.data.map(x => {
                        return {...x, createdOn: moment(x.createdOn).format("YYYY-MM-DD")}
                    });
                })
                .catch(err => {
                    console.log(err.message);
                });
        },
        methods: {
            search(){

            },
            show(id) {
                this.$router.push({name: "workflow-view",params:{id}});
            },
            create() {
                this.$router.push({name: "workflow-create"});
            },
            edit({id,name,type}) {
                this.$router.push({name: "workflow-edit",params:{id,name,type}});
            },
            remove(id,index) {
                this.loading = true;
                this.$axios.delete(`${api.workflow}/${id}`).then(response => {
                    this.loading = false;
                    this.$message({
                        message: "删除成功",
                        type: "success"
                    });
                    this.workflows.splice(index,1);
                }).catch(err => {
                    this.loading = false;
                    const result = response.response.data;
                    this.$message({
                        message: "删除失败：" + result.Message,
                        type: "error"
                    });
                });
            }
        }
    };
</script>
<style>
</style>
