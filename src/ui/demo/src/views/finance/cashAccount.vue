<template>
    <section class="cashAccount">
        <el-row>
            <el-col :span="24">
                <div class="contentTitle">
                    <i class="el-icon-search"></i>
                    账单信息查询
                </div>
            </el-col>
        </el-row>

        <el-row class="search-row el-form el-form--inline inputBox">
            <el-col class="listRow" style="margin-top: 10px">
                    <label>项目： </label>
                     <div class="filter-content" style="height:auto">
                         <el-select v-model="name" placeholder="请选择">
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
                            <el-input placeholder="客户姓名" style="display: inline-block"></el-input>
                        </div>
                        <label>账单月份： </label>
                        <div class="filter-content" style="height:auto">
                            <el-date-picker style="display: inline-block;"
                                            v-model="value4"
                                            type="month"
                                            placeholder="选择月">
                            </el-date-picker>
                        </div>
                <el-button type="primary"  icon="el-icon-search">查询</el-button>

            </el-col>
            <el-col :span="24">
                <label>状态： </label>
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
                <label>账单类型： </label>
                <div class="filter-content" style="height:auto">
                    <el-select v-model="type" placeholder="请选择">
                        <el-option
                                v-for="item in accountType"
                                :key="item.value"
                                :label="item.label"
                                :value="item.value">
                        </el-option>
                    </el-select>
                </div>
            </el-col>
        </el-row>
        <div style="border:1px solid #ffffff;margin-top: 30px">
            <el-row :gutter="20" style="margin-top: 20px;">
                <el-col :span="15" style="margin-left: 15px;box-sizing: border-box; color: #409EFF;"><i class="el-icon-document"></i>账单信息列表</el-col>
                <el-col :span="8" style="text-align: right">
                    <el-button type="primary" size="mini" icon="el-icon-success" :disabled="multipleSelection.length === 0">账单确认</el-button>
                    <el-button type="primary" size="mini" icon="el-icon-refresh" :disabled="multipleSelection.length === 0">重新计算</el-button>
                    <el-button type="primary" size="mini" icon="el-icon-download">导出</el-button>
                </el-col>
            </el-row>
            <el-row style="margin-top: 20px;">
                <el-table
                        ref="multipleTable"
                        :data="tableData"
                        tooltip-effect="dark"
                        style="width: 100%"
                        @selection-change="handleSelectionChange">
                    <el-table-column
                            type="selection"
                            width="55">
                    </el-table-column>
                    <el-table-column
                            prop="a"
                            label="账单类型"
                            width="120">
                        <!--<template slot-scope="scope">{{ scope.row.date }}</template>-->
                    </el-table-column>
                    <el-table-column
                            prop="b"
                            label="账单日期"
                            width="120">
                    </el-table-column>
                    <el-table-column
                            prop="c"
                            label="客户编号"
                            show-overflow-tooltip>
                    </el-table-column>
                    <el-table-column
                            prop="d"
                            label="客户姓名"
                            show-overflow-tooltip>
                    </el-table-column>
                    <el-table-column
                            prop="e"
                            label="合同编号"
                            show-overflow-tooltip>
                    </el-table-column>
                    <el-table-column
                            prop="f"
                            label="账单状态"
                            show-overflow-tooltip>
                    </el-table-column>
                    <el-table-column
                            prop="g"
                            label="账单金额"
                            show-overflow-tooltip>
                    </el-table-column>
                    <el-table-column
                            prop="h"
                            label="已缴金额"
                            show-overflow-tooltip>
                    </el-table-column><el-table-column
                        prop="i"
                        label="未缴金额"
                        show-overflow-tooltip>
                </el-table-column>
                    <el-table-column fixed="right" label="操作">
                        <template slot-scope="scope">
                            <el-button @click="dialogVisible = true" type="text">生成调整账单</el-button>
                        </template>
                    </el-table-column>
                </el-table>
                <el-pagination
                        @size-change="handleSizeChange"
                        @current-change="handleCurrentChange"
                        :current-page="currentPage4"
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
    export default{
        data(){
            return {
                currentPage4: 1,
                value4: '',
                name:'',
                status:'',
                type:'',
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
                accountType: [{
                    value: '选项1',
                    label: '黄金糕'
                }, {
                    value: '选项2',
                    label: '双皮奶'
                }],
                tableData: [
                    {
                        a: '减免账单',
                        b: '2017年11月',
                        c: '00011310',
                        d: '杜长芬',
                        e: 'CXM-BJSQ-RZ-20170304',
                        f: '待审批',
                        g: '-2706.67',
                        h: '0.00',
                        i: '-2706.67',
                    },
                    {
                        a: '减免账单',
                        b: '2017年11月',
                        c: '00011310',
                        d: '杜长芬',
                        e: 'CXM-BJSQ-RZ-20170304',
                        f: '待审批',
                        g: '-2706.67',
                        h: '0.00',
                        i: '-2706.67',
                    },
                    {
                        a: '减免账单',
                        b: '2017年11月',
                        c: '00011310',
                        d: '杜长芬',
                        e: 'CXM-BJSQ-RZ-20170304',
                        f: '待审批',
                        g: '-2706.67',
                        h: '0.00',
                        i: '-2706.67',
                    },
                    {
                        a: '减免账单',
                        b: '2017年11月',
                        c: '00011310',
                        d: '杜长芬',
                        e: 'CXM-BJSQ-RZ-20170304',
                        f: '待审批',
                        g: '-2706.67',
                        h: '0.00',
                        i: '-2706.67',
                    }
                    ],
                multipleSelection:[]
            }

            },
        methods :{
            handleSizeChange(val) {
                console.log(`每页 ${val} 条`);
            },
            handleCurrentChange(val) {
                console.log(`当前页: ${val}`);
            },
            handleSelectionChange(val) {
                this.multipleSelection = val;
            },

        }
        }
</script>
<style lang="scss">
.cashAccount {
    /*.inputBox {*/
        /*@media screen and (max-width:1366px){*/
            /*.el-input--small,.el-select > .el-input {*/
                /*width: 128px;*/
            /*}*/
            /*.el-date-editor.el-input {*/
                /*width: 126px;*/
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

} 
    
</style>
<style lang="scss" scoped>
.cashAccount {
      .contentTitle{
          background: #f5f7fa;
          padding: 5px;
          box-sizing:border-box;
          color: #606266;
          font-size: 14px;
      }
    .customName {
        display: inline-block;
        width: 60%;
    }
    .listRow {
        label {
            float: none;
            display: inline-block;
            text-align: right;
            vertical-align: middle;
            font-size: 12px;
            color: #606266;
            line-height: 35px;
            padding: 0 12px 0 0;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
        }

    }
.active {
    background: indianred;
}

}

      

</style>