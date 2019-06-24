<template>
    <section class="cashFlow">
            <el-row :gutter="20" style="margin-top: 20px;">
                <el-col :span="20" style="margin-left: 15px;box-sizing: border-box; color: #409EFF;"><i class="el-icon-document"></i>积分信息</el-col>
                <el-col :span="2"><el-button type="primary" size="mini" icon="el-icon-circle-plus" v-if="$hasPermission('新增')">新增</el-button></el-col>
            </el-row>
            <el-row style="margin-top: 20px;">
                <el-table
                        ref="multipleTable"
                        :data="tableData3"
                        tooltip-effect="dark"
                        style="width: 100%"
                        @selection-change="handleSelectionChange">
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
    </section>
</template>
<script>
    import SearchButton from "./../../components/SearchButton.vue";
    export default {
        data(){
            return {
                show:false,
                value6: '',
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
                options: [{
                    value: '选项1',
                    label: '黄金糕'
                }, {
                    value: '选项2',
                    label: '双皮奶'
                }, {
                    value: '选项3',
                    label: '蚵仔煎'
                }, {
                    value: '选项4',
                    label: '龙须面'
                }, {
                    value: '选项5',
                    label: '北京烤鸭'
                }],
                value: '',
                currentPage:1,
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
    .cashFlow {
        .el-range-editor--small.el-input__inner {
            width: 51%;
        }

    }

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
    }

</style>