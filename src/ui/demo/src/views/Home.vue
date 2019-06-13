<template>
    <el-container class="big_home">
        <el-aside width="70%" class="asideContent">
            <el-row :gutter="20" style="margin-right: 0px;margin-top: 10px">
                <el-col :span="6">
                    <el-col style="margin-left: 0;margin-right: 0;">
                        <el-col :span="8" class="percent">
                            <i class="fa fa-user" aria-hidden="true"></i>
                        </el-col>
                        <el-col :span="16" class="percentContent">
                            <p class="titlerate_">今日咨询</p>
                            <p class="titlerate">666</p>
                        </el-col>
                    </el-col>
                </el-col>
                <el-col :span="6" style="padding-right: 0px;">
                    <el-col style="margin-left: 0;margin-right: 0;">
                        <el-col :span="8" class="percent percent_">
                            <i class="fa fa-envelope-open"></i>
                        </el-col>
                        <el-col :span="16" class="percentContent">
                            <p class="titlerate_">今日签约</p>
                            <p class="titlerate">222</p>
                        </el-col>
                    </el-col>
                </el-col>
                <el-col :span="6">
                    <el-col style="margin-left: 0;margin-right: 0;">
                        <el-col :span="8" class="percent percent__">
                            <i class="fa fa-leaf" aria-hidden="true"></i>
                        </el-col>
                        <el-col :span="16" class="percentContent">
                            <p class="titlerate_">今日退住</p>
                            <p class="titlerate">222</p>
                        </el-col>
                    </el-col>
                </el-col>
                <el-col :span="6" style="padding-right: 0px;">
                    <el-col style="margin-left: 0;margin-right: 0;">
                        <el-col :span="8" class="percent percent___">
                            <i class="fa fa-server" aria-hidden="true"></i>
                        </el-col>
                        <el-col :span="16" class="percentContent">
                            <p class="titlerate_">今日服务</p>
                            <p class="titlerate">222</p>
                        </el-col>
                    </el-col>
                </el-col>
            </el-row>
            <el-row :gutter="20" style="margin-top: 10px;margin-right: 0px;margin-left: 0px;">
                <el-col :span="12">
                        <el-card class="box-card">
                            <div slot="header" class="clearfix">
                                <span><i class="el-icon-info"></i> 欠费提醒</span>
                                <el-table
                                        :data="ArrearageTips"
                                        height="250"
                                        style="width: 100%;margin-top: 10px">
                                    <el-table-column
                                            prop="apartment"
                                            label="公寓">
                                    </el-table-column>
                                    <el-table-column
                                            prop="name"
                                            label="客户姓名">
                                    </el-table-column>
                                    <el-table-column
                                            prop="Arrearage"
                                            label="欠费金额">
                                    </el-table-column>
                                </el-table>
                            </div>
                        </el-card>
                </el-col>
                <el-col :span="12">
                    <el-card class="box-card">
                        <div slot="header">
                            <span><i class="el-icon-tickets"></i> 合同催办提醒</span>
                            <el-table
                                    :data="ArrearageTips"
                                    height="250"
                                    style="width: 100%;margin-top: 10px">
                                <el-table-column
                                        prop="apartment"
                                        label="公寓"
                                        width="180">
                                </el-table-column>
                                <el-table-column
                                        prop="name"
                                        label="客户姓名"
                                        width="180">
                                </el-table-column>
                                <el-table-column
                                        prop="Arrearage"
                                        label="房间床位">
                                </el-table-column>
                                <el-table-column>
                                <template slot-scope="scope">
                                        <span style="margin-left: 10px">
                                        <el-tag type="danger">合同已过期</el-tag>
                                        </span>
                                </template>
                                </el-table-column>

                            </el-table>
                        </div>
                    </el-card>
                </el-col>
            </el-row>
            <el-row :span="24"  class="charts" style="margin: 20px auto;">
                    <div id="homeChart"></div>
            </el-row>
        </el-aside>
        <el-main class="mainLeft">
            <el-row :span="24">
                <el-card class="box-card">
                    <div slot="header" class="clearfix">
                        <span><i class="el-icon-document"></i> 待办事项</span>
                        <el-button style="float: right; padding: 3px 0" type="text">查看更多</el-button>
                    </div>
                    <el-table
                            :data="willDo"
                            height="250"
                            style="width: 100%">
                        <el-table-column
                                prop="number"
                                width="120"
                                label="序号">
                            <template slot-scope="scope">
                                <i class="el-icon-sort" style="color: #4FC9CA"></i>&nbsp;<span style="color: #4FC9CA">{{scope.row.number}}</span>
                            </template>
                        </el-table-column>
                        <el-table-column
                                label="待办事项">
                            <template slot-scope="scope">
                                <i class="el-icon-message" style="color: #4FC9CA"></i>&nbsp;<span style="color: #4FC9CA">{{scope.row.dosomething}}</span>
                            </template>
                        </el-table-column>
                    </el-table>
                </el-card>
            </el-row>
            <el-row :span="24"  style="margin-top: 20px">
                <el-card class="box-card">
                    <div slot="header" class="clearfix">
                        <span><i class="el-icon-date"></i> 通知公告</span>
                        <el-button style="float: right; padding: 3px 0" type="text">查看更多</el-button>
                    </div>
                    <el-table
                            :data="inform"
                            height="250"
                            style="width: 100%">
                        <el-table-column
                                prop="time"
                                width="120"
                                label="时间">
                            <template slot-scope="scope">
                                <i class="el-icon-time" style="color:#409EFF"></i>&nbsp;<span style="color:#409EFF">{{scope.row.time}}</span>
                            </template>

                        </el-table-column>
                        <el-table-column
                                prop="title"
                                label="消息">
                            <template slot-scope="scope">
                                <i class="el-icon-message" style="color: #409EFF"></i>&nbsp;<span style="color:#409EFF">{{scope.row.title}}</span>
                            </template>
                        </el-table-column>
                    </el-table>
                </el-card>
            </el-row>
        </el-main>
    </el-container>
</template>
<script>
    export default {
        data(){
            return {
                willDo: [{
                    number: '1',
                    dosomething: '西安天合老年公寓 张三丰入住合同审批 不同意',

                }, {
                    number: '2',
                    dosomething: '西安天合老年公寓 张三丰入住合同审批 不同意',

                },{
                    number: '3',
                    dosomething: '西安天合老年公寓 张三丰入住合同审批 不同意',

                },{
                    number: '4',
                    dosomething: '西安天合老年公寓 张三丰入住合同审批 不同意',

                }],
                inform: [{
                    title: '1.西安天合老年公寓 张三丰入住合同审批 不同意',
                    time: '2018.12.13',

                }, {
                    title: '2.西安天合老年公寓 张三丰入住合同审批 不同意',
                    time: '2018.12.13',

                },{
                    title: '3.西安天合老年公寓 张三丰入住合同审批 不同意',
                    time: '2018.12.13',

                },{
                    title: '4.西安天合老年公寓 张三丰入住合同审批 不同意',
                    time: '2018.12.13',

                },{
                    title: '5.西安天合老年公寓 张三丰入住合同审批 不同意',
                    time: '2018.12.13',

                },{
                    title: '6.西安天合老年公寓 张三丰入住合同审批 不同意',
                    time: '2018.12.13',

                },{
                    title: '7.西安天合老年公寓 张三丰入住合同审批 不同意',
                    time: '2018.12.13',

                }],
                ArrearageTips: [{
                    apartment: '公寓1',
                    name: '张三',
                    Arrearage: '12.00'
                },{
                    apartment: '公寓1',
                    name: '张三',
                    Arrearage: '12.00'
                },{
                    apartment: '公寓1',
                    name: '张三',
                    Arrearage: '12.00'
                },{
                    apartment: '公寓1',
                    name: '张三',
                    Arrearage: '12.00'
                }],
                urge:[{
                    apartment: '公寓1',
                    name: '张三',
                    Arrearage: '1'
                },{
                    apartment: '公寓1',
                    name: '张三',
                    Arrearage: '3'
                },{
                    apartment: '公寓1',
                    name: '张三',
                    Arrearage: '6'
                },{
                    apartment: '公寓1',
                    name: '张三',
                    Arrearage: '2'
                }],
            }
        },
        methods :{
            gethomeChart (){
                let option = {
                    title: {
                        text: '入住统计折线图'
                    },
                    tooltip: {
                        trigger: 'axis'
                    },
                    legend: {
                        data:['公寓1','公寓2','公寓3','公寓4','公寓5']
                    },
                    grid: {
                        left: '3%',
                        right: '4%',
                        bottom: '3%',
                        containLabel: true
                    },
                    // toolbox: {
                    //     feature: {
                    //         saveAsImage: {}
                    //     }
                    // },
                    xAxis: {
                        type: 'category',
                        boundaryGap: false,
                        data: ['周一','周二','周三','周四','周五','周六','周日']
                    },
                    yAxis: {
                        type: 'value'
                    },
                    series: [
                        {
                            name:'公寓1',
                            type:'line',
                            stack: '总量',
                            data:[120, 132, 101, 134, 90, 230, 210]
                        },
                        {
                            name:'公寓2',
                            type:'line',
                            stack: '总量',
                            data:[220, 182, 191, 234, 290, 330, 310]
                        },
                        {
                            name:'公寓3',
                            type:'line',
                            stack: '总量',
                            data:[150, 232, 201, 154, 190, 330, 410]
                        },
                        {
                            name:'公寓4',
                            type:'line',
                            stack: '总量',
                            data:[320, 332, 301, 334, 390, 330, 320]
                        },
                        {
                            name:'公寓5',
                            type:'line',
                            stack: '总量',
                            data:[820, 932, 901, 934, 1290, 1330, 1320]
                        }
                    ]
                };
                var myChart = this.$echarts.init(document.getElementById('homeChart'));
                myChart.setOption(option, true)
                myChart.resize()
            }

    },
        mounted (){
            this.gethomeChart()
        }
    }
</script>
<style lang="scss">
    .big_home {
        ::-webkit-scrollbar{
        height: 0;
        width: 0;
        color: transparent;
        }
        /*.el-table thead{*/
            /*color: #666;*/
        /*}*/
        /*tbody {*/
            /*color: #666;*/
        /*}*/
        .el-row{
            padding: 0!important;
        }
        .el-col-24 {
            padding: 10px;
        }
        .mainLeft{
            .box-card .el-card__header {
                padding:15px 15px 5px 15px;
                box-sizing: border-box;
                color: #666;
            }
            .el-table th {
                background: #ffffff;
            }
            .el-table th > .cell ,.el-table .cell{
                padding:0 15px;
            }
            .el-card__header {
                border-bottom: none;
            }
            .el-table th.is-leaf, .el-table td {
                border-bottom: 1px solid #fafafa;
            }
            .el-button--text {
                color: #666;
            }
            .el-table thead{
                color: #666;
            }

        }
        .asideContent {
            .box-card .el-card__header {
                padding-left:10px;
                padding-right:10px;
                box-sizing: border-box;
            }
            .el-card__header {
                padding-left:10px;
                padding-right:10px;
                border-bottom: none;
            }

        }
    }
</style>
<style lang="scss" scoped>
    .big_home {
        background: #FAFAFA;
        .percent {
            height: 90px;
            background: #4FC9CA;
            position: relative;
            i {
                font-size: 40px;
                color: #fff;
                position: absolute;
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%);

            }
        }
        .percent_ {
            background: #FF6E5D;
        }
        .percent__ {
             background: #EFC849;
         }
        .percent___ {
             background: #A2EBE0;
         }



        .percentContent {
            height: 90px;
            background: #ffffff;
            position: relative;
            color: #999;
            .titlerate {
                position: absolute;
                top: 0%;
                left: 50%;
                transform: translate(-50%, -50%);
                font-size: 30px;
            }
            .titlerate_ {
                position: absolute;
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%);
                font-size: 14px;

            }
        }
        .mainLeft {

            .icon_span {
                color: orangered;
            }
        }
        .charts {
            background: #fff;
            width: 98%;
            border-radius: 5px;
            #homeChart {
                width: 100%;
                height: 500px;

            }
        }

    }

</style>
