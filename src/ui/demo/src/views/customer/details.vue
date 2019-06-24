<template>
    <section style="background: #FAFAFA">
        <el-container class="content_">
            <el-aside width="300px" class="aside_">
                <el-row class="details-row">
                    <el-col :span="24" class="img-radius img-radius--customer avater">
                        <img src="https://chjwebserver.oss-cn-beijing.aliyuncs.com/avatar/3aa3e667-fab2-4e7b-adfe-e112c72ed57d_30586998.jpg">
                    </el-col>
                    <el-row class="elBox">
                        <el-col :span="24" style="margin-top: 10px;padding-left: 20px;padding-right: 10px;"
                                class="allP">
                            <p class="font_p" style="font-weight: 600;color: #409EFF;font-size: 18px">
                                {{customer.name}}</p>
                            <!--<p class="font_p" style="font-weight: 600;color: #409EFF;font-size: 18px">{{contract.apartmentName}}</p>-->
                            <p class="p_font">
                                <span>公寓名称</span>
                                <span class="text-ar">诚和敬公寓</span>
                            </p>
                            <p class="p_font">
                                <span>年龄</span>
                                <span class="text-ar">{{customer.age}}</span>
                            </p>
                            <p class="p_font">
                                <span>状态</span>
                                <span class="text-ar">请假回家</span>
                            </p>
                            <p class="p_font">
                                <span>生活能力类型</span>
                                <span class="text-ar">{{customer.liveTypeName}}</span>
                            </p>
                            <p class="p_font no_boder">
                                <span>入住时间</span>
                                <span class="text-ar">{{moment(customer.startLiveTime).format("ll")}}</span>
                            </p>
                        </el-col>
                        <!--<el-col :span="20" style="border-bottom:1px dashed #ccc;height: 1px;margin:10px 0 0 26px;"></el-col>-->
                        <div class="ml13" style="padding-right: 10px">
                            <el-col :span="24" class="button-top-row" style="margin-top: 10px;padding-left: 0px">
                                <el-col :span="8">
                                    <el-dropdown trigger="click" class="el-dropdown-first"
                                                 style="margin-top: -5px;padding: 7px 10px 0px 0px;">
                                        <el-button type="primary" size="mini" icon="el-icon-news" class="letter">客户
                                        </el-button>
                                        <el-dropdown-menu slot="dropdown">
                                            <el-dropdown-item>黄金糕</el-dropdown-item>
                                            <el-dropdown-item>狮子头</el-dropdown-item>
                                            <el-dropdown-item>螺蛳粉</el-dropdown-item>
                                            <el-dropdown-item>双皮奶</el-dropdown-item>
                                            <el-dropdown-item>蚵仔煎</el-dropdown-item>
                                        </el-dropdown-menu>
                                    </el-dropdown>
                                </el-col>
                                <el-col :span="8">
                                    <el-button type="primary" size="mini" @click="create('customerFamily')"
                                               icon="el-icon-phone-outline" class="letter">亲属
                                    </el-button>
                                </el-col>
                                <el-col :span="8">
                                    <el-button type="primary" size="mini" @click="create" icon="el-icon-sort"
                                               class="letter">转账
                                    </el-button>
                                </el-col>
                            </el-col>
                            <el-col :span="24" style="padding-left: 0px">
                                <el-col :span="8">
                                    <el-button type="primary" size="mini" @click="create" icon="el-icon-tickets"
                                               class="letter">付款
                                    </el-button>
                                </el-col>
                                <el-col :span="8">
                                    <el-button type="primary" size="mini" @click="create" icon="el-icon-share"
                                               class="letter">续签
                                    </el-button>
                                </el-col>
                                <el-col :span="8">
                                    <el-button type="primary" size="mini" @click="create" icon="el-icon-circle-close"
                                               class="letter">退住
                                    </el-button>
                                </el-col>
                            </el-col>
                            <el-col :span="24" style="padding-left: 0px">
                                <el-col :span="8">
                                    <el-button type="primary" size="mini" @click="create" icon="el-icon-sold-out"
                                               class="letter">餐饮
                                    </el-button>
                                </el-col>
                                <el-col :span="8">
                                    <el-button type="primary" size="mini" @click="create" icon="el-icon-setting"
                                               class="letter">销假
                                    </el-button>
                                </el-col>
                                <el-col :span="8">
                                    <el-button type="primary" size="mini" @click="create" icon="el-icon-sort-down">
                                        账单减免
                                    </el-button>
                                </el-col>
                            </el-col>
                            <el-col :span="24" style="padding-left: 0px">
                                <el-col :span="8">
                                    <el-button type="primary" size="mini" @click="create('appreciationService')"
                                               icon="el-icon-service">增值服务
                                    </el-button>
                                </el-col>
                                <el-col :span="8">
                                    <el-dropdown v-if="customer.contractId" @command="agreementHandleCommand"
                                                 trigger="click" class="el-dropdown-first" style="margin-top: -6px;">
                                        <el-button type="primary" size="mini" icon="el-icon-document">补充协议</el-button>
                                        <el-dropdown-menu slot="dropdown">
                                            <el-dropdown-item command="cost">费用变更补充协议</el-dropdown-item>
                                            <el-dropdown-item command="service">服务包补充协议</el-dropdown-item>
                                            <el-dropdown-item command="room">换房补充协议</el-dropdown-item>
                                        </el-dropdown-menu>
                                    </el-dropdown>
                                </el-col>
                            </el-col>

                        </div>
                    </el-row>
                </el-row>
            </el-aside>
            <el-container>
                <el-header class="head_">
                    <el-row :gutter="20" style="margin-right: 0px;margin-top: 10px">
                        <el-col :span="6" class="border_r">
                            <el-col style="margin-left: 0;margin-right: 0;">

                                <el-col :span="8" class="percent">
                                    <i class="fa fa-jpy" aria-hidden="true">￥</i>
                                </el-col>
                                <el-col :span="16" class="percentContent">
                                    <p class="titlerate_">现金余额</p>
                                    <p class="titlerate color_price">666.00</p>
                                </el-col>
                            </el-col>
                            <!--<p class="bg-purple">现金余额</p>-->
                            <!--<p class="bg-purple-light">24.00</p>-->
                        </el-col>
                        <el-col :span="6" class="border_r">
                            <el-col style="margin-left: 0;margin-right: 0;">
                                <el-col :span="8" class="percent percent_">
                                    <i class="fa fa-calculator" aria-hidden="true"></i>
                                </el-col>
                                <el-col :span="16" class="percentContent">
                                    <p class="titlerate_">待确认金额</p>
                                    <p class="titlerate color_price_">200.00</p>
                                </el-col>
                            </el-col>
                            <!--<p class="bg-purple">待确认金额</p>-->
                            <!--<p class="bg-purple-light">200.00</p>-->
                        </el-col>
                        <el-col :span="6" class="border_r">
                            <el-col style="margin-left: 0;margin-right: 0;">
                                <el-col :span="8" class="percent percent__">
                                    <i class="fa fa-database" aria-hidden="true"></i>
                                </el-col>
                                <el-col :span="16" class="percentContent">
                                    <p class="titlerate_">积分余额</p>
                                    <p class="titlerate color_price__">0</p>
                                </el-col>
                            </el-col>
                            <!--<p class="bg-purple">积分余额</p>-->
                            <!--<p class="bg-purple-light">0</p>-->
                        </el-col>
                        <el-col :span="6" class="border_r">
                            <el-col style="margin-left: 0;margin-right: 0;">
                                <el-col :span="8" class="percent percent___">
                                    <i class="fa fa-gbp" aria-hidden="true">$</i>
                                </el-col>
                                <el-col :span="16" class="percentContent">
                                    <p class="titlerate_">押金余额</p>
                                    <p class="titlerate color_price___">666.00</p>
                                </el-col>
                            </el-col>
                            <!--<p class="bg-purple">押金余额</p>-->
                            <!--<p class="bg-purple-light">8000.00</p>-->
                        </el-col>
                    </el-row>
                </el-header>
                <el-main>
                    <el-row>
                        <el-tabs type="border-card">
                            <el-tab-pane label="基本信息">
                                <el-row class="details-row">
                                    <el-collapse v-model="activeNames">
                                        <el-collapse-item title="基本信息" name="1">
                                            <el-col :span="8">
                                                <label>房间号：</label>
                                                <span>{{customer.roomType}}</span>
                                                <!-- contract.bedName -->
                                            </el-col>
                                            <el-col :span="8">
                                                <label>客户编号：</label>
                                                <span>{{customer.customerNo}}</span>
                                            </el-col>
                                            <el-col :span="8">
                                                <label>入住日期：</label>
                                                <span>{{moment(customer.startLiveTime).format("ll")}}</span>
                                            </el-col>
                                            <el-col :span="8">
                                                <label>姓名：</label>
                                                <span>{{customer.name}}</span>
                                            </el-col>
                                            <el-col :span="8">
                                                <label>性别：</label>
                                                <span>{{customer.sexText}}</span>
                                            </el-col>
                                            <el-col :span="8">
                                                <label>客户生日：</label>
                                                <span>{{moment(customer.birthday).format("ll")}}</span>
                                            </el-col>
                                            <el-col :span="8">
                                                <label>联系电话：</label>
                                                <span>{{customer.phone}}</span>
                                            </el-col>
                                            <el-col :span="8">
                                                <label>证件类型：</label>
                                                <span>{{customer.credentialType==1?"居民身份证":"护照"}}</span>
                                            </el-col>
                                            <el-col :span="8">
                                                <label>证件号：</label>
                                                <span>{{customer.idCard}}</span>
                                            </el-col>
                                            <el-col :span="8">
                                                <label>电子邮箱：</label>
                                                <span>{{customer.email}}</span>
                                            </el-col>
                                            <el-col :span="16">
                                                <label>联系地址：</label>
                                                <span>{{customer.address}}</span>
                                            </el-col>
                                        </el-collapse-item>
                                    </el-collapse>
                                </el-row>
                            </el-tab-pane>
                            <el-tab-pane label="亲属信息">
                                <CheckRelative :id="customer.id" @from="checkRelativeFrom"
                                               :refresh="componentRefresh.checkRelative"></CheckRelative>
                            </el-tab-pane>
                            <el-tab-pane label="请假信息">
                                <askForleave></askForleave>
                            </el-tab-pane>
                            <el-tab-pane label="增值服务">
                                <ValueAddedService :id="customer.id"></ValueAddedService>
                            </el-tab-pane>
                            <el-tab-pane label="合同&附加协议">
                                <Contract v-model="customer.id"></Contract>
                            </el-tab-pane>
                            <el-tab-pane label="账单信息">
                                <BillingInfo></BillingInfo>
                            </el-tab-pane>
                            <el-tab-pane label="账户信息">
                                <AccountInformation></AccountInformation>
                            </el-tab-pane>
                            <el-tab-pane label="入住记录">
                                <CheckRecord></CheckRecord>
                            </el-tab-pane>
                            <el-tab-pane label="费率记录">
                                <RatesRecords></RatesRecords>
                            </el-tab-pane>
                            <el-tab-pane label="医嘱信息">
                                <doctorAdvice></doctorAdvice>
                            </el-tab-pane>
                            <el-tab-pane label="药单信息">
                                <MedicineInformation></MedicineInformation>
                            </el-tab-pane>
                        </el-tabs>
                    </el-row>
                </el-main>
            </el-container>
        </el-container>
        <!-- <el-row class="button-bottom-row">
                  <el-button type="primary">立即创建</el-button>
                  <el-button>取消</el-button>
        </el-row>-->
        <el-dialog :close-on-click-modal="false" title="亲属信息" :visible.sync="dialogVisible.customerFamily" width="30%">
            <el-form ref="customerFamilyForm" :rules="rules.customerFamily" :model="form.customerFamily"
                     label-width="85px">
                <el-form-item label="姓名：" prop="name">
                    <el-input v-model="form.customerFamily.name" placeholder="请输入姓名"></el-input>
                </el-form-item>
                <el-form-item label="性别：" prop="sex">
                    <el-select v-model="form.customerFamily.sex" style="width: 100%;" placeholder="请选择性别">
                        <el-option label="女" :value="2"></el-option>
                        <el-option label="男" :value="1"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="关系：" prop="asTypes">
                    <ConfigurationSelector v-model="form.customerFamily.asTypes" type="销售项配置" name="与客户关系"
                                           style="width:100%;" placeholder="请选择与乙方关系"></ConfigurationSelector>
                </el-form-item>
                <el-form-item label="证件号：" prop="idCardNo">
                    <el-input v-model="form.customerFamily.idCardNo" placeholder="请输入证件号"></el-input>
                </el-form-item>
                <el-form-item label="手机号码：" prop="phone">
                    <el-input v-model="form.customerFamily.phone" placeholder="请输入手机号码"></el-input>
                </el-form-item>
                <el-form-item label="固定电话：">
                    <el-input v-model="form.customerFamily.tel" placeholder="请输入固定电话"></el-input>
                </el-form-item>
                <el-form-item label="工作单位：">
                    <el-input v-model="form.customerFamily.workUnit" placeholder="请输入工作单位"></el-input>
                </el-form-item>
                <el-form-item label="电子邮箱：" prop="email">
                    <el-input v-model="form.customerFamily.email" placeholder="请输入电子邮箱"></el-input>
                </el-form-item>
                <el-form-item label="地址：">
                    <el-input v-model="form.customerFamily.address" placeholder="请输入地址"></el-input>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="cancel('customerFamily')">取 消</el-button>
                <el-button type="primary" @click="customerFamilySave">确 定</el-button>
            </div>
        </el-dialog>
        <el-dialog :close-on-click-modal="false" title="增值服务" :visible.sync="dialogVisible.appreciationService"
                   width="60%">
            <AppreciationService
                    :customerId="customer.id"
                    :apartmentId="customer.apartmentId"
                    :visible="dialogVisible.appreciationService"
                    @callback="appreciationServiceVisible"></AppreciationService>
        </el-dialog>
        <ChangeLiveType :id="customer.contractId" :visible="dialogVisible.liveType"
                        @callback="liveTypeVisible"></ChangeLiveType>
        <ChangeRoom :id="customer.contractId" :visible="dialogVisible.room" @callback="roomVisible"></ChangeRoom>
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

    import CheckRelative from "../../components/CheckRelative.vue";
    import askForleave from "../../components/askForleave.vue";
    import ValueAddedService from "../../components/ValueAddedService.vue";
    import Contract from "../../components/contract.vue";
    import BillingInfo from "../../components/BillingInfo.vue";
    import CheckRecord from "../../components/CheckRecord.vue";
    import RatesRecords from "../../components/RatesRecords.vue";
    import AccountInformation from "../../components/AccountInformation.vue";
    import doctorAdvice from "../../components/doctorAdvice.vue";
    import MedicineInformation from "../../components/MedicineInformation.vue";
    import ChangeLiveType from "../../components/ChangeLiveType.vue";
    import ChangeRoom from "../../components/ChangeRoom.vue";
    import ConfigurationSelector from "../../components/ConfigurationSelector.vue";
    import AppreciationService from "../../components/AppreciationService.vue";
    import validators from "../../common/validators";

    import {TextDecoder} from "util";
    import api from "../../config/api";
    import moment from "moment";

    export default {
        components: {
            CheckRelative,
            askForleave,
            ValueAddedService,
            Contract,
            BillingInfo,
            CheckRecord,
            RatesRecords,
            AccountInformation,
            doctorAdvice,
            MedicineInformation,
            ChangeLiveType,
            ChangeRoom,
            AppreciationService,
            ConfigurationSelector
        },
        data() {
            return {
                dialogVisible: {
                    appreciationService: false,
                    customerFamily: false,
                    liveType: false,
                    room: false
                },
                disabledType: false,
                activeNames: ["1"],
                form: {
                    appreciationService: {},
                    customerFamily: {}
                },
                componentRefresh: {
                    checkRelative: false
                },
                response: {},
                customer: {},
                rules: {
                    customerFamily: {
                        name: [{required: true, message: "请输入姓名", trigger: "blur"}],
                        sex: [{required: true, message: "请选择性别", trigger: "change"}],
                        asTypes: [{required: true, message: "请选择关系", trigger: "change"}],
                        phone: [
                            {required: true, message: "请输入手机号码", trigger: "blur"},
                            {validator: validators.phone, trigger: "blur"}
                        ],
                        idCardNo: [
                            {validator: validators.idNumber, trigger: "blur"}
                        ],
                        email: [
                            {
                                type: "email",
                                message: "电子邮箱格式错误",
                                trigger: "blur"
                            }
                        ]
                    }
                }
            };
        },

        async mounted() {
            if (this.$route.params.id) {
                const response = await this.$axios.get(
                    api.customer + "/" + this.$route.params.id
                );
                this.customer = _.clone(response.data);
            }
        },

        methods: {
            // 接收父组件传过来的参数
            appreciationServiceVisible(e) {
                this.dialogVisible.appreciationService = e.visible;
                if (e.status === true) {
                    // this.search();
                }
            },
            // 接收父组件传过来的参数
            liveTypeVisible(e) {
                this.dialogVisible.liveType = e.visible;
                if (e.status === true) {
                    // this.search();
                }
            },
            // 接收父组件传过来的参数
            roomVisible(e) {
                this.dialogVisible.room = e.visible;
                if (e.status === true) {
                    // this.search();
                }
            },
            agreementHandleCommand(command) {
                switch (command) {
                    case "cost":
                        break;
                    case "service":
                        this.dialogVisible.liveType = true;
                        break;
                    case "room":
                        this.dialogVisible.room = true;
                        break;
                }
            },
            typeChange(val) {
                // this.disabledType = val === "1" ? true : false;
                this.disabledType = true;
            },

            create(type) {
                switch (type) {
                    case "customerFamily":
                        // 重置表单
                        this.form.customerFamily = {};
                        if (this.$refs["customerFamilyForm"]) {
                            this.$refs["customerFamilyForm"].clearValidate(); // 重置验证
                        }
                        this.dialogVisible.customerFamily = true;
                        break;
                    case "appreciationService":
                        this.dialogVisible.appreciationService = true;
                        break;
                    default:
                        break;
                }
            },


            cancel(type) {
                switch (type) {
                    case "customerFamily":
                        // 重置表单
                        this.dialogVisible.customerFamily = false;
                        this.componentRefresh.checkRelative = false;
                        break;
                    case "appreciationService":
                        this.dialogVisible.appreciationService = false;
                        break;
                    default:
                        break;
                }
            },
            // 接收父组件传过来的参数
            checkRelativeFrom(e) {
                switch (e.status) {
                    case "edit":
                        this.dialogVisible.customerFamily = e.dialog;
                        this.form.customerFamily = e.data;
                        if (this.$refs["customerFamilyForm"]) {
                            this.$refs["customerFamilyForm"].clearValidate(); // 重置验证
                        }
                        break;
                    case "delete":
                        break;
                }
            },
            customerFamilySave() {
                // command
                this.$refs.customerFamilyForm.validate(valid => {
                    if (valid) {
                        // console.log(this.command.startTime);
                        this.saving = true;
                        let command = _.clone(this.form.customerFamily);
                        command.customerId = this.customer.id;
                        const httpRequest = command.id
                            ? this.$axios.put(api.customerFamily, command)
                            : this.$axios.post(api.customerFamily, command);
                        httpRequest.then(
                            response => {
                                this.loading = false;
                                this.$message({
                                    message: "保存成功",
                                    type: "success"
                                });
                                this.componentRefresh.checkRelative = true;
                                this.dialogVisible.customerFamily = false;
                            }
                        ).catch(err => {
                            this.loading = false;
                            this.$message({
                                type: "error",
                                message: "保存失败:" + err.message,
                                showClose: true,
                                duration: 0
                            });
                        });
                    } else {
                        return false;
                    }
                });
            }
        }
    };
</script>
<style lang="scss">
    .content_ {
        .elBox {
            padding: 10px 10px 30px 0px;
            margin-top: 20px;
            border-radius: 3px;
            /*.ml13 {
                margin-left: -16px;
            }*/
        }

        .details-row .elBox .el-col {
            /*padding:0 10px 0 20px;*/
            margin-bottom: 5px;
        }

        .el-button--mini {
            padding: 7px 5px;
        }
    }
</style>
<style lang="scss" scoped>
    .content_ {
        .letter {
            letter-spacing: 8px;
        }

        .avater {
            width: 100%;
            height: 120px;
            position: relative;
            /*background: #6CD2E9;*/
            img {
                width: 110px;
                height: 110px;
                position: absolute;
                top: 75%;
                left: 50%;
                transform: translate(-50%, -50%);
            }
        }

        .el-header {
            height: 130px !important;
        }

        .head_ {
            text-align: center;
            /*background: #fff;*/
            margin: 20px 20px 0px 20px;
            box-sizing: border-box;

            .border_r {
                padding: 10px;
                position: relative;
                /*background: #fff;*/
                /*margin-right: 10px;*/
                border-radius: 5px;
            }

            .border_nor {
                padding: 10px;
            }

            /* .border_r:before {
                 position: absolute;
                 content: "";
                 width: 1px;
                 height: 70%;
                 background: #ccc;
                 transform: translate(-50%, -50%);
                 top: 45%;
                 right: 0;
             }*/

            .bg-purple {
                font-size: 16px;
                color: #666;
            }

            .bg-purple-light {
                font-size: 26px;
                color: #409eff;
            }

            .percent {
                height: 90px;
                background: #4FC9CA;
                position: relative;
                /*border-radius: 50%;*/
                /*width: 90px;*/
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

                .color_price {
                    color: #409EFF;
                }

                .color_price_ {
                    color: #FF6E5D;
                }

                .color_price__ {
                    color: #EFC849;
                }

                .color_price___ {
                    color: #A2EBE0;
                }
            }
        }

        .elBox {
            .allP {
                p {
                    text-align: center;
                    color: #666666;
                }

                .font_p {
                    font-size: 16px;
                    margin-bottom: 30px;
                }

                .p_font {
                    font-size: 12px;
                    margin-bottom: 5px;
                    height: 10px;
                    border-bottom: 1px solid #ddd;
                    padding-bottom: 20px;

                    span {
                        display: inline-block;
                        float: left;
                        width: 50%;
                        text-align: left;
                    }

                    .text-ar {
                        text-align: right;
                    }
                }

                .no_boder {
                    border: none;
                }
            }
        }

        .aside_ {
            background: #fff;
            margin: 20px 0px 20px 20px;
            box-sizing: border-box;
        }
    }
</style>
