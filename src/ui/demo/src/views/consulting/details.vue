<template>
    <section>
        <el-row class="details-row">
            <el-collapse v-model="activeNames">
                <el-collapse-item title="客户基础信息" name="1">
                    <el-col :span="8">
                        <label>咨询类型：</label>
                        <span>{{consulting.consultingTypeText}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>客户类型：</label>
                        <span>{{consulting.consultingCustomerTypeText}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>状态：</label>
                        <span>{{consulting.consultingStatusText}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>获知渠道：</label>
                        <span>{{consulting.consultingCustomerTypeText}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>意向公寓：</label>
                        <span>{{consulting.apartmentName}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>意向户型：</label>
                        <span>{{consulting.roomTypeName}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>入住需求：</label>
                        <span>{{consulting.isCompartment?'包房':'拼房'}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>身体状况：</label>
                        <span>{{consulting.healthText}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>工作行业：</label>
                        <span>{{consulting.industryText}}</span>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="客户其他信息" name="2">
                    <el-col :span="8">
                        <label>客户姓名：</label>
                        <span>{{consulting.name}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>性别：</label>
                        <span>{{consulting.sexText}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>年龄：</label>
                        <span>{{consulting.age}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>联系电话：</label>
                        <span>{{consulting.phone}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>居住区域：</label>
                        <span>{{consulting.area}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>联系地址：</label>
                        <span>{{consulting.address}}</span>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="推荐人信息" name="3">
                    <el-col :span="8">
                        <label>推荐类型：</label>
                        <span>{{consulting.referralTypeText}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>推荐渠道：</label>
                        <span>{{consulting.referralChannelText}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>推荐人：</label>
                        <span>{{consulting.referrerName}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>联系方式：</label>
                        <span>{{consulting.referrerPhone}}</span>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item v-if="consulting.consultingType == 2" title="咨询人信息" name="4">
                    <el-col :span="8">
                        <label>姓名：</label>
                        <span>{{consulting.consultantName}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>联系电话：</label>
                        <span>{{consulting.consultantPhone}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>居住区域：</label>
                        <span>{{consulting.consultantArea}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>联系地址：</label>
                        <span>{{consulting.consultantAddress}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>关系：</label>
                        <span>{{consulting.consultantRelationText}}</span>
                    </el-col>
                    <el-col :span="24">
                        <label>备注：</label>
                        <span>{{consulting.consultantRemark}}</span>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item name="5">
                    <template slot="title">
                        客户跟踪列表
                    </template>
                    <el-col :span="24">
                        <el-table :data="consulting.traces" style="width: 100%;padding:auto 10px;">
                            <el-table-column label="序号">
                                <template slot-scope="scope">
                                    {{scope.$index + 1}}
                                </template>
                            </el-table-column>
                            <
                            <el-table-column label="所属公寓">
                                <template slot-scope="scope">{{consulting.apartmentName}}</template>
                            </el-table-column>
                            <el-table-column prop="typeText" label="沟通类型"></el-table-column>
                            <el-table-column label="开始日期">
                                <template slot-scope="scope">
                                    {{moment(scope.row.startTime).format("YYYY-MM-DD")}}
                                </template>
                            </el-table-column>
                            <el-table-column label="结束日期">
                                <template slot-scope="scope">
                                    {{moment(scope.row.endTime).format("YYYY-MM-DD")}}
                                </template>
                            </el-table-column>
                            <el-table-column prop="seller" label="销售人员">
                                <template slot-scope="scope">{{consulting.sellerName}}</template>
                            </el-table-column>
                            <el-table-column prop="record" label="沟通记录"></el-table-column>
                            <!--<el-table-column label="是否完成">-->
                                <!--<template slot-scope="scope">-->
                                    <!--{{scope.row.finished?"是":"否"}}-->
                                <!--</template>-->
                            <!--</el-table-column>-->
                        </el-table>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item name="6">
                    <template slot="title">
                        转交记录
                    </template>
                    <el-col :span="24">
                        <el-table :data="consulting.transferRecords" style="width: 100%;padding:auto 10px;">
                            <el-table-column label="序号">
                                <template slot-scope="scope">
                                    {{scope.$index + 1}}
                                </template>
                            </el-table-column>
                            <el-table-column label="所属公寓">
                                <template slot-scope="scope">{{consulting.apartmentName}}</template>
                            </el-table-column>
                            <el-table-column prop="fromUser" label="转交人"></el-table-column>
                            <el-table-column prop="toUser" label="转交给"></el-table-column>
                            <el-table-column label="转交日期">
                                <template slot-scope="scope">
                                    {{moment(scope.row.date).format("YYYY-MM-DD")}}
                                </template>
                            </el-table-column>
                            <el-table-column prop="reason" label="转交原因"></el-table-column>
                        </el-table>
                    </el-col>
                </el-collapse-item>
            </el-collapse>
            <el-row class="button-bottom-row">
                <el-button @click="goBack">返回</el-button>
            </el-row>

        </el-row>
    </section>
</template>
<script>
    import {
        provinceAndCityData,
        regionData,
        provinceAndCityDataPlus,
        regionDataPlus,
        CodeToText,
        TextToCode
    } from "element-china-area-data";

    export default {
        data() {
            return {
                disabledType: false,
                activeNames: ["1", "2", "3", "4", "5", "6"],

                moreSearch: false,
                currentPage: 15,
                filters: {},
                consulting: {
                    traces: [],
                    transferRecords:[],
                    apartment: {},
                    roomType: {}
                },
                options: regionData,
                selectedOptions: []
            };
        },

        async mounted() {
            const response = await this.$axios.get("consulting/" + this.$route.params.id);
            this.consulting = response.data;
        },
        methods: {
            typeChange(val) {
                // this.disabledType = val === "1" ? true : false;
                this.disabledType = true;
            },
            goBack() {
                this.$router.go(-1);
            }
        }
    };
</script>
<style>
</style>
