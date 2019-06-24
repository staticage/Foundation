<style lang="scss">
    .traceIndex {
        .inputBox {
            @media screen and (max-width: 1366px) {
                .el-input--small, .el-select > .el-input ,.el-select,.el-select el-select--small{
                    width: 150px;
                }
                /*.el-date-editor.el-input {*/
                    /*width: 126px;*/
                /*}*/
                   /*.el-select {*/
                        /*width: 150px;*/
                    /*}*/

                /*.el-select el-select--small {*/
                    /*width: 150px;*/
                /*}*/
            }
            @media screen and (min-width: 1920px) {
                .el-input--small, .el-range-editor--small.el-input__inner {
                    width: 178px;
                }
                /*.el-date-editor.el-input {*/
                    /*width: 178px;*/
                /*}*/
            }
        }
    }
</style>
<style lang="scss" scoped></style>
<template>
    <section class="traceIndex">
        <el-row class="search-row el-form el-form--inline inputBox">
            <el-col :span="24">
                <label>公寓名称：</label>
                <div class="filter-content" style="height:auto">
                    <el-select size="small" v-if="apartmentList" v-model="query.apartmentId">
                        <el-option v-for="item in apartmentList" :key="item.value" :label="item.text"
                                   :value="item.value">{{item.text}}
                        </el-option>
                    </el-select>
                </div>
                <label>客户名称：</label>
                <div class="filter-content" style="height:auto">
                    <el-input v-model="query.customerName" placeholder="客户名称"></el-input>
                </div>
                <label>咨询人名称：</label>
                <div class="filter-content" style="height:auto">
                    <el-input v-model="query.consultantName" placeholder="咨询人名称"></el-input>
                </div>

                <el-button type="primary" @click="search" :loading="loading" icon="el-icon-search">查询</el-button>
                <el-button type="info" plain @click="reset" icon="el-icon-refresh">重置</el-button>
            </el-col>
            <el-col :span="24">
                <label>沟通类型：</label>
                <ConfigurationSelector v-model="query.traceType" type="销售项配置" name="追踪类型"></ConfigurationSelector>
                <label>销售名称：</label>
                <div class="filter-content" style="height:auto">
                    <el-input v-model="query.sellerName" placeholder="销售名称"></el-input>
                </div>
                <label>跟&nbsp;踪&nbsp;时&nbsp;&nbsp;间：</label>
                <!--<div class="filter-content" style="height:auto">-->
                    <!--<el-date-picker v-model="query.startTime" type="date" placeholder="选择日期"></el-date-picker>-->
                <!--</div>-->
                <!--<label>结束时间：</label>-->
                <!--<div class="filter-content" style="height:auto">-->
                    <!--<el-date-picker v-model="query.endTime" type="date" placeholder="选择日期"></el-date-picker>-->
                <!--</div>-->
                <div class="filter-content" style="height:auto">
                    <el-date-picker
                            style="width: 220px"
                            v-model="times"
                            type="daterange"
                            @change="getTime"
                            range-separator="-"
                            start-placeholder="开始日期"
                            end-placeholder="结束日期">
                    </el-date-picker>
                </div>


            </el-col>
        </el-row>
        <el-row class="table-row">
            <el-table :data="traces">
                <el-table-column type="index" width="50"></el-table-column>
                <el-table-column prop="apartmentName" label="所属项目"></el-table-column>
                <el-table-column prop="customerName" label="客户姓名"></el-table-column>
                <el-table-column prop="consultantName" label="咨询人名称"></el-table-column>
                <el-table-column prop="traceTypeText" label="沟通类型"></el-table-column>

                <el-table-column label="开始日期">
                    <template slot-scope="scope">{{moment(scope.row.startTime).format("YYYY-MM-DD")}}
                    </template>
                </el-table-column>
                <el-table-column label="结束日期">
                    <template slot-scope="scope">{{moment(scope.row.endTime).format("YYYY-MM-DD")}}
                    </template>
                </el-table-column>
                <el-table-column prop="sellerName" label="销售人员"></el-table-column>
                <el-table-column prop="record" label="跟踪记录"></el-table-column>
                <!-- <el-table-column prop="g" label="销售人员"></el-table-column> -->
                <el-table-column label="操作">
                    <template slot-scope="scope">
                        <el-button @click="view(scope.row.consultingId)" type="text">查看</el-button>
                        <el-button @click="remove(scope.row.id)" type="text"
                                   v-if="($hasPermission('删除') || scope.row.belongMe)">删除
                        </el-button>
                    </template>
                </el-table-column>
            </el-table>
            <el-pagination
                    :current-page="query.page"
                    :page-sizes="[10, 20, 30, 40]"
                    :page-size="query.pageSize"
                    layout="total,prev,pager,next,sizes,jumper"
                    :total="total"
                    @size-change="handleSizeChange"
                    @current-change="handleCurrentChange"
            ></el-pagination>
        </el-row>
    </section>
</template>
<script>
    import SearchButton from "../../components/SearchButton.vue";
    import ConfigurationSelector from "../../components/ConfigurationSelector.vue";
    import api from "../../config/api";

    export default {
        components: {
            SearchButton,
            ConfigurationSelector
        },
        data() {
            return {
                times:'',
                currentPage: 15,
                query: {},
                loading: false,
                traces: [],
                apartmentList: [],
                total: 0
            };
        },

        async mounted() {
            this.search();
        },
        methods: {
            getTime (){

            },
            handleSizeChange(pageSize) {
                this.query.pageSize = pageSize;
                this.search();
            },

            handleCurrentChange(page) {
                this.query.page = page;
                this.search();
            },
            search() {
                this.loading = true;
                if (this.times !=='') {
                    this.query.startTime = this.times[0]
                    this.query.endTime= this.times[1]
                }
                this.$axios.post(api.consultingTraces, this.query).then(response => {
                    this.traces = response.data.items;
                    this.total = response.data.total;
                    this.loading = false;
                }).catch(err => {
                    this.loading = false;
                    this.$message({
                        message: "加载出错，请重试",
                        type: "error"
                    });
                })
                this.$axios.get(api.apartmentDropdown).then(response => {
                    this.apartmentList = response.data.map(x => {
                        return {
                            text: x.name,
                            value: x.id
                        };
                    });
                }).catch(err => {
                    console.log(err.message);
                });
            },
            reset (){
                this.query={};
                this.times=[];
            },
            view(id) {
                this.$router.push({
                    name: "consulting-details",
                    params: {id}
                });
            },

            async remove(id) {
                this.$confirm("此操作将永久删除此追踪记录, 是否继续?", "提示", {
                    confirmButtonText: "确定",
                    cancelButtonText: "取消",
                    type: "warning"
                }).then(() => {
                    this.$axios
                        .delete(`${api.deleteTrace}/${id}`)
                        .then(response => {
                            this.$message({
                                type: "success",
                                message: "删除成功!"
                            });
                            let index = _.findIndex(this.traces, x => x.id == id);
                            this.traces.splice(index, 1);
                        })
                        .catch(err => {
                            this.$message({
                                type: "error",
                                message: err.message
                            });
                        });
                }).catch(() => {
                });
            }
        }
    };
</script>
