<template>
    <section class="cashFlow">
        <el-row>
            <el-col :span="24">
                <div class="contentTitle">
                    <i class="el-icon-search"></i>
                    客户现金流水信息查询
                </div>
            </el-col>
        </el-row>
        <el-row class="search-row el-form el-form--inline inputBox">
            <el-col :gutter="20" class="listRow" style="margin-top: 20px">
                    <label>项&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;目&nbsp;： </label>
                        <div class="filter-content" style="height:auto">
                            <el-select v-model="projectTitle" placeholder="请选择">
                                <el-option
                                        v-for="item in projectNames"
                                        :key="item.value"
                                        :label="item.label"
                                        :value="item.value">
                                </el-option>
                            </el-select>
                        </div>
                    <label>客户姓名： </label>
                        <div class="filter-content" style="height:auto">
                            <el-input placeholder="客户姓名" v-model="customName" style="display: inline-block;"></el-input>
                        </div>
                     <label>发起人：</label>
                     <div class="filter-content" style="height:auto">
                         <el-input placeholder="客户姓名"></el-input>
                     </div>
                     <label>收支日期： </label>
                     <div class="filter-content" style="height:auto">
                         <el-date-picker
                                 style="width: 230px"
                                 v-model="value6"
                                 type="daterange"
                                 range-separator="-"
                                 start-placeholder="开始日期"
                                 end-placeholder="结束日期">
                         </el-date-picker>
                     </div>
            </el-col>
            <el-col :gutter="20" class="listRow" >
                    <label>确认人&nbsp;&nbsp;&nbsp; ：</label>
                    <div class="filter-content" style="height:auto">
                        <el-input placeholder="客户姓名"></el-input>
                    </div>

                     <label>收支方式：</label>
                            <div class="filter-content" style="height:auto">
                                <el-select v-model="income" placeholder="请选择">
                                    <el-option
                                            v-for="item in incomes"
                                            :key="item.value"
                                            :label="item.label"
                                            :value="item.value">
                                    </el-option>
                                </el-select>
                            </div>
                    <label>状&nbsp;&nbsp;&nbsp;态 ：</label>
                    <div class="filter-content" style="height:auto">
                        <el-select v-model="status" placeholder="请选择">
                            <el-option
                                    v-for="item in projectStatus"
                                    :key="item.value"
                                    :label="item.label"
                                    :value="item.value">
                            </el-option>
                        </el-select>
                    </div>


                    <label>到账日期：</label>
                    <div class="filter-content" style="height:auto">
                        <el-date-picker
                                style="width: 230px"
                                v-model="value6"
                                type="daterange"
                                range-separator="-"
                                start-placeholder="开始日期"
                                end-placeholder="结束日期">
                        </el-date-picker>
                    </div>
                </el-col>
            <el-col :gutter="20" class="listRow">
                        <label>备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注 ：</label>
                             <div class="filter-content" style="height:auto">
                                 <el-select v-model="value" placeholder="请选择">
                                     <el-option
                                             v-for="item in notes"
                                             :key="item.value"
                                             :label="item.label"
                                             :value="item.value">
                                     </el-option>
                                 </el-select>
                             </div>
                        <label>收支金额：</label>
                             <div class="filter-content" style="height:auto">
                                 <el-input placeholder="请输入收支金额" v-model="customName" style="display: inline-block;"></el-input>
                             </div>
                        <el-button type="primary"  @click="search" :loading="loading" icon="el-icon-search">查询</el-button>
                        <el-button type="info" plain @click="query={}" icon="el-icon-refresh">重置</el-button>
            </el-col>
                <!--<SearchButton @input="showInput" class="more"></SearchButton>-->
        </el-row>
        <div style="border:1px solid #ffffff;margin-top: 30px">
            <el-row :gutter="20" style="margin-top: 20px;">
                <el-col :span="18" style="margin-left: 15px;box-sizing: border-box; color: #409EFF;"><i class="el-icon-document"></i>客户现金流水管理</el-col>
                <el-col :span="5" style="text-align: right">
                    <el-button type="primary" size="mini" icon="el-icon-download">导出</el-button>
                    <el-button type="primary" size="mini" icon="el-icon-success" :disabled="multipleSelection.length===0">批量确认</el-button>
                </el-col>
            </el-row>
            <el-row style="margin-top: 20px;">
                <el-table
                        ref="multipleTable"
                        :data="tableData3"
                        tooltip-effect="dark"
                        style="width: 100%"
                        @selection-change="handleSelectionChange">
                    <el-table-column
                            type="selection">
                    </el-table-column>
                    <el-table-column
                            prop="a"
                            label="序号">
                        <!--<template slot-scope="scope">{{ scope.row.date }}</template>-->
                    </el-table-column>
                    <el-table-column
                            prop="b"
                            label="项目">
                    </el-table-column>
                    <el-table-column
                            prop="c"
                            label="客户姓名"
                            show-overflow-tooltip>
                    </el-table-column>
                    <el-table-column
                            prop="d"
                            label="收支日期"
                            show-overflow-tooltip>
                    </el-table-column>
                    <el-table-column
                            prop="e"
                            label="到账日期"
                            show-overflow-tooltip>
                    </el-table-column>
                    <el-table-column
                            prop="f"
                            label="收支方式"
                            show-overflow-tooltip>
                    </el-table-column>
                    <el-table-column
                            prop="g"
                            label="收入金额"
                            show-overflow-tooltip>
                    </el-table-column>
                    <el-table-column
                            prop="h"
                            label="支出金额"
                            show-overflow-tooltip>
                    </el-table-column>
                    <el-table-column
                        prop="i"
                        label="备注"
                        show-overflow-tooltip>
                     </el-table-column>
                    <el-table-column
                            prop="g"
                            label="收支人"
                            show-overflow-tooltip>
                    </el-table-column>
                    <el-table-column
                            prop="k"
                            label="确认人"
                            show-overflow-tooltip>
                    </el-table-column>
                    <el-table-column
                            prop="l"
                            label="状态"
                            show-overflow-tooltip>
                    </el-table-column>
                    <el-table-column fixed="right" label="操作">
                        <template slot-scope="scope">
                            <el-button @click="dialogVisible = true" type="text">确认</el-button>
                        </template>
                    </el-table-column>
                </el-table>
                <el-pagination
                        :current-page="currentPage"
                        :page-sizes="[100, 200, 300, 400]"
                        :page-size="100"
                        layout="total, sizes, prev, pager, next, jumper"
                        :total="400">
                </el-pagination>
            </el-row>
        </div>

    </section>
</template>
<script>
    import SearchButton from "./../../components/SearchButton.vue";
    export default {
        data(){
            return {
                // show:false,
                value6: '',
                projectTitle:'',
                customName:'',
                status:'',
                income:'',
                incomes:'',
                tableData3: [
                    {
                        a: '1',
                        b: '亦庄',
                        c: '王文魁',
                        d: '2017年11月',
                        e: '2017年11月',
                        f: '现金',
                        g: '-2706.67',
                        h: '0.00',
                        i: '预收费用',
                        j: '超级管理员',
                        k: '超级管理员',
                        l: '待确认'
                    },
                    {
                        a: '1',
                        b: '亦庄',
                        c: '王文魁',
                        d: '2017年11月',
                        e: '2017年11月',
                        f: '现金',
                        g: '-2706.67',
                        h: '0.00',
                        i: '预收费用',
                        j: '超级管理员',
                        k: '超级管理员',
                        l: '待确认'
                    },
                    {
                        a: '1',
                        b: '亦庄',
                        c: '王文魁',
                        d: '2017年11月',
                        e: '2017年11月',
                        f: '现金',
                        g: '-2706.67',
                        h: '0.00',
                        i: '预收费用',
                        j: '超级管理员',
                        k: '超级管理员',
                        l: '待确认'
                    },
                    {
                        a: '1',
                        b: '亦庄',
                        c: '王文魁',
                        d: '2017年11月',
                        e: '2017年11月',
                        f: '现金',
                        g: '-2706.67',
                        h: '0.00',
                        i: '预收费用',
                        j: '超级管理员',
                        k: '超级管理员',
                        l: '待确认'
                    },
                ],
                projectNames: [{
                    value: '选项1',
                    label: '黄金糕'
                }, {
                    value: '选项2',
                    label: '双皮奶'
                }],
                projectStatus: [{
                    value: '选项1',
                    label: '黄金糕'
                }, {
                    value: '选项2',
                    label: '双皮奶'
                }],
                value: '',
                currentPage:1,
                multipleSelection:[]
            }
        },
        methods :{
            showInput(item){
                if (item === true) {
                    this.show = true
                }else {
                    this.show = false
                }

            },
            handleSelectionChange(val) {
                this.multipleSelection = val;
            }
        },
        components :{
            SearchButton
        },
    }
</script>
<style lang="scss">
    /*.cashFlow {*/
        /*.inputBox {*/
            /*@media screen and (max-width:1366px){*/
                /*.el-input--small,.el-range-editor--small.el-input__inner {*/
                    /*width: 175px;*/
                /*}*/
            /*}*/
            /*@media screen and (min-width:1920px){*/
                /*.el-input--small,.el-range-editor--small.el-input__inner {*/
                    /*width: 210px;*/
                /*}*/
            /*}*/
            /*.el-input__inner {*/
                /*height: 28px;*/
                /*line-height:28px;*/
            /*}*/
        /*}*/

    /*}*/

</style>
<style lang="scss" scoped>
    .cashFlow {
        .contentTitle{
            background: #f5f7fa;
            padding: 5px;
            box-sizing:border-box;
            color: #606266;
            font-size: 14px;
        }
        .listRow {
            /*label {*/
                /*float: none;*/
                /*display: inline-block;*/
                /*text-align: right;*/
                /*vertical-align: middle;*/
                /*font-size: 12px;*/
                /*color: #606266;*/
                /*line-height: 35px;*/
                /*padding: 0 12px 0 0;*/
                /*-webkit-box-sizing: border-box;*/
                /*box-sizing: border-box;*/
            /*}*/

        }
        .more {
            padding-top: 10px;
        }
    }

</style>