<template>
    <section style="margin-top:-10px;">
        <el-row class="details-row">
            <el-collapse v-model="activeNames">
                <el-collapse-item title="合同基本信息" name="1">
                    <el-row>
                        <el-col :span="8">
                            <label>客户编号：</label>
                            <span>{{command.customerNo}}</span>
                        </el-col>
                        <!-- 合同编号：编辑时可查看即可 -->
                        <el-col :span="8">
                            <label>合同编号：</label>
                            <span>{{command.contractNo}}</span>
                        </el-col>
                        <el-col :span="8">
                            <label>签约销售：</label>
                            <span>{{command.creatorName}}</span>
                        </el-col>
                    </el-row>
                    <el-row>
                        <el-col :span="8">
                            <label>签署类型：</label>
                            <span>{{command.signedType==1?"新签":"续签"}}</span>
                            <!-- <el-form-item label prop="signedType">
                <el-select v-model="command.signedType" placeholder="请选择签署类型" style="width:100%">
                  <el-option label="新签" :value="1"></el-option>
                  <el-option label="续签" :value="2"></el-option>
                </el-select>
                            </el-form-item>-->
                        </el-col>
                        <el-col :span="8">
                            <label>合同开始日期：</label>
                            <span>{{moment(command.startTime).format("ll")}}</span>
                        </el-col>
                        <el-col :span="8">
                            <label>合同截至日期：</label>
                            <span>{{moment(command.startTime).format("ll")}}</span>
                        </el-col>
                        <el-col :span="8">
                            <label>签约日期：</label>
                            <span>{{moment(command.signedOn).format("ll")}}</span>
                        </el-col>
                    </el-row>
                </el-collapse-item>
                <el-collapse-item title="甲方信息（养老服务机构）" name="2">
                    <el-row>
                        <el-col :span="8">
                            <label>公寓名称：</label>
                            <span>{{command.apartmentName}}</span>
                        </el-col>
                    </el-row>
                    <el-row>
                        <!--<el-col :span="8">-->
                            <!--<label>甲方：</label>-->
                            <!--<span>{{apartment.name}}</span>-->
                        <!--</el-col>-->
                        <el-col :span="8">
                            <label>法定代表人：</label>
                            <span>{{apartment.corporation}}</span>
                        </el-col>
                        <el-col :span="8">
                            <label>联系电话：</label>
                            <span>{{apartment.phone}}</span>
                        </el-col>
                        <el-col :span="8">
                            <label>银行户名：</label>
                            <span>{{apartment.accountName}}</span>
                        </el-col>
                        <el-col :span="8">
                            <label>开户行：</label>
                            <span>{{apartment.bankName}}</span>
                        </el-col>
                        <el-col :span="8">
                            <label>银行账号：</label>
                            <span>{{apartment.accountNumber}}</span>
                        </el-col>
                        <el-col :span="8">
                            <label>电子邮箱：</label>
                            <span>{{apartment.email}}</span>
                        </el-col>
                        <el-col :span="8">
                            <label>邮政编码：</label>
                            <span>{{apartment.postcode}}</span>
                        </el-col>
                        <!--<el-col :span="8">-->
                            <!--<label>地址：</label>-->
                            <!--<span>{{apartment.address}}</span>-->
                        <!--</el-col>-->
                    </el-row>
                </el-collapse-item>
                <el-collapse-item title="乙方（入住老年人）" name="3">
                    <el-col :span="8">
                        <label>乙方姓名：</label>
                        <span>{{command.bName}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>性别：</label>
                        <span>{{command.bSex==1?"男":"女"}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>年龄：</label>
                        <span>{{command.bAge}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>证件类型：</label>
                        <span>{{command.bCredentialType==1?"居民身份证":"护照"}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>证件号：</label>
                        <span>{{command.biDcard}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>联系电话：</label>
                        <span>{{command.bPhone}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>联系地址：</label>
                        <span>{{command.bArea?command.bArea.join(' '):"" }} {{command.bAddress}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>电子邮箱：</label>
                        <span>{{command.bEmail}}</span>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="乙方监护人" name="4">
                    <el-col :span="8">
                        <label>乙方监护人姓名：</label>
                        <span>{{command.cName}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>性别：</label>
                        <span>{{command.cSex==1?"男":"女"}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>年龄：</label>
                        <span>{{command.cAge}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>与乙方关系：</label>
                        <span>{{command.cAsTypesName}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>证件类型：</label>
                        <span>{{command.cCredentialType==1?"居民身份证":"护照"}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>证件号：</label>
                        <span>{{command.ciDcard}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>手机号码：</label>
                        <span>{{command.cPhone}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>固定电话：</label>
                        <span>{{command.cTel}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>工作单位：</label>
                        <span>{{command.cCompany}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>电子邮箱：</label>
                        <span>{{command.cEmail}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>通讯地址：</label>
                        <span>{{command.cArea?command.cArea.join(' '):""}} {{command.cAddress}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>邮政编码：</label>
                        <span>{{command.cAddress}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>经常居住地地址：</label>
                        <span>{{command.cLiveArea?command.cLiveArea.join(' '):""}} {{command.cLiveAddress}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>乙方监护人（非自然人）名称：</label>
                        <span>{{command.cLegalPersonCompany}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>法人代表：</label>
                        <span>{{command.cLegalPersonName}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>电子邮箱：</label>
                        <span>{{command.cLegalPersonEmail}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>联系人：</label>
                        <span>{{command.cLegalPersonContactName}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>手机号码：</label>
                        <span>{{command.cLegalPersonPhone}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>固定电话：</label>
                        <span>{{command.cLegalPersonTel}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>公司地址：</label>
                        <span>{{command.cLegalPersonArea?command.cLegalPersonArea.join(' '):"" }} {{command.cLegalPersonAddress}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>邮政编码：</label>
                        <span>{{command.cLegalPersonPostcode}}</span>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="丙方" name="5">
                    <el-row>
                        <el-col :span="8">
                            <label>丙方作为入住老人的：</label>
                            <span>{{command.dRelationship}}</span>
                        </el-col>
                        <el-col :span="8">
                            <label>丙方姓名：</label>
                            <span>{{command.dName}}</span>
                        </el-col>
                        <el-col :span="8">
                            <label>性别：</label>
                            <span>{{command.dSex==1?"男":"女"}}</span>
                        </el-col>
                    </el-row>
                    <el-row>
                        <el-col :span="8">
                            <label>与乙方关系：</label>
                            <span>{{command.dAsTypesName}}</span>
                        </el-col>
                        <el-col :span="8">
                            <label>证件类型：</label>
                            <span>{{command.dCredentialType==1?"居民身份证":"护照"}}</span>
                        </el-col>
                        <el-col :span="8">
                            <label>证件号：</label>
                            <span>{{command.diDcard}}</span>
                        </el-col>
                    </el-row>
                    <el-row>
                        <el-col :span="8">
                            <label>手机号码：</label>
                            <span>{{command.dPhone}}</span>
                        </el-col>
                        <el-col :span="8">
                            <label>固定电话：</label>
                            <span>{{command.dTel}}</span>
                        </el-col>
                        <el-col :span="8">
                            <label>工作单位：</label>
                            <span>{{command.dCompany}}</span>
                        </el-col>
                    </el-row>
                    <el-row>
                        <el-col :span="8">
                            <label>电子邮箱：</label>
                            <span>{{command.dEmail}}</span>
                        </el-col>
                        <el-col :span="8">
                            <label>通讯地址：</label>
                            <span>{{command.dArea?command.dArea.join(' '):""}} {{command.dAddress}}</span>
                        </el-col>
                    </el-row>
                    <el-col :span="8">
                        <label>邮政编码：</label>
                        <span>{{command.dPostcode}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>经常居住地区域：</label>
                        <span>{{command.dLiveArea?command.dLiveArea.join(' '):""}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>经常居住地地址：</label>
                        <span>{{command.dLiveAddress}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>丙方（非自然人）名称：</label>
                        <span>{{command.dLegalPersonCompany}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>法人代表：</label>
                        <span>{{command.dLegalPersonName}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>电子邮箱：</label>
                        <span>{{command.dLegalPersonEmail}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>联系人：</label>
                        <span>{{command.dLegalPersonContactName}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>手机号码：</label>
                        <span>{{command.dLegalPersonPhone}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>固定电话：</label>
                        <span>{{command.dLegalPersonTel}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>公司地址：</label>
                        <span>{{command.dLegalPersonArea?command.dLegalPersonArea.join(' '):""}} {{command.dLegalPersonAddress}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>邮政编码：</label>
                        <span>{{command.dLegalPersonPostcode}}</span>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="房间信息" name="6">
                    <el-col :span="8">
                        <label>是否包房：</label>
                        <span>{{command.isCompartment==true ?"包房":"拼房"}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>床位：</label>
                        <span>{{command.buildingNames?command.buildingNames.join(","):""}}</span>
                        <!-- <el-form-item label prop="buildings">
              <el-cascader
                :options="apartment.buildings"
                v-model="command.buildings"
                style="width:100%;"
                @change="changeBuilding"
              ></el-cascader>
                        </el-form-item>-->
                    </el-col>
                    <el-col :span="8">
                        <label>房型：</label>
                        <span>{{command.roomType}}</span>
                    </el-col>
                    <el-col :span="24">
                        <label>服务地点：</label>
                        <span>{{command.apartmentAddress}}</span>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="费用包" name="7">
                    <el-col :span="8">
                        <label>费用包：</label>
                        <span>{{command.pricePackage.name}}</span>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="服务项目" v-if="serviceVisible" name="8">
                    <el-row>
                        <el-col :span="8">
                            <label>生活能力类型：</label>
                            <span>{{command.liveTypeName}}</span>
                        </el-col>
                        <el-col :span="8">
                            <label>押金类型：</label>
                            <span>{{depositType}}</span>
                            <!-- {{command.depositType}} -->
                        </el-col>
                    </el-row>
                </el-collapse-item>
                <el-collapse-item title="收费信息" v-if="serviceVisible" name="9">
                    <el-col :span="8">
                        <label>押金：</label>
                        <span>{{command.depositCost}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>一次性安置费：</label>
                        <span>{{prices.relocationCost}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>床位费：</label>
                        <span>{{command.roomCost}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>餐费：</label>
                        <span>{{prices.mealsCost}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>基础服务费：</label>
                        <span>{{prices.serviceCost}}</span>
                    </el-col>
                    <el-col :span="8">
                        <!-- 生活能力类型 -->
                        <label>生活能力费用：</label>
                        <span>{{command.serviceMonthlyCost}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>附加服务费：</label>
                        <span>{{prices.attachCost}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>违约金：</label>
                        <span>{{prices.liquidatedDamages}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>请假退餐费：</label>
                        <span>{{prices.refundCost}}</span>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="老带新信息" name="10">
                    <el-col :span="8">
                        <label>推荐人：</label>
                        <span>{{command.customerId}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>推荐人奖励积分：</label>
                        <span>{{command.recommendPoint}}</span>
                    </el-col>
                    <el-col :span="8">
                        <label>被推荐人奖励积分：</label>
                        <span>{{command.bonusPoint}}</span>
                    </el-col>
                </el-collapse-item>
            </el-collapse>
        </el-row>
        <el-row class="button-bottom-row">
            <el-button @click="goBack">返回</el-button>
        </el-row>
    </section>
</template>
<script>
import ConfigurationSelector from "../../components/ConfigurationSelector.vue";
import AreaSelector from "../../components/AreaSelector.vue";
import api from "../../config/api";
import moment from "moment";
export default {
    components: {
        ConfigurationSelector,
        AreaSelector
    },
    props: {
        contractId: {
            type: String
        }
    },
    data() {
        return {
            // contractId: "",
            contract: {},
            contractTemporary: {},
            serviceVisible: false,
            disabledType: false,
            // company: {},
            prices: {},
            activeNames: ["1", "2", "3", "4", "5", "6", "7", "8", "9"],
            apartmentList: null,
            roomTypeList: null,
            saving: false,
            displayState: { chargeDescription: false },
            apartments: [],
            apartment: {
                buildings: []
            },
            consulting: {},
            pricePackages: [],
            selectOption: {
                pricePackages: [],
                roomTypes: [],
                depositTypes: [],
                liveTypes: []
            },
            depositType: "",
            command: {
                signedType: 1,
                apartmentId: "",
                b2AsTypes: 0,
                cAsTypes: "",
                pricePackageId: "",
                pricePackage: [],
                buildings: [],
                bSex: "",
                isCompartment: false,
                liveType: "",
                depositType: "",
                bedId: "",
                cRelationship: [],
                signedOn: new Date()
            }
        };
    },

    async mounted() {
        if(this.$route.params.id != undefined){
            this.contractId = this.$route.params.id;
        }
        if (this.contractId != undefined) {
            // this.company = (await this.$axios.get("company/current")).data;
            this.contract = (await this.$axios.get(
                "contract/" + this.contractId
            )).data;
            this.command = _.clone(this.contract);

            if (this.command.signedOn)
                this.command.signedOn = moment(this.command.signedOn).toDate();
            if (this.command.endTime)
                this.command.endTime = moment(this.command.endTime).toDate();
            if (this.command.startTime)
                this.command.startTime = moment(
                    this.command.startTime
                ).toDate();
            this.command.bArea = this.command.bArea.split(",");
            if (this.command.cArea) {
                this.command.cArea = this.command.cArea.split(",");
            }

            if (this.command.cLiveArea) {
                this.command.cLiveArea = this.command.cLiveArea.split(",");
            }
            if (this.command.cLegalPersonArea) {
                this.command.cLegalPersonArea = this.command.cLegalPersonArea.split(
                    ","
                );
            }
            if (this.command.dLiveArea) {
                this.command.dLiveArea = this.command.dLiveArea.split(",");
            }
            if (this.command.dArea) {
                this.command.dArea = this.command.dArea.split(",");
            }
            if (this.command.dLegalPersonArea) {
                this.command.dLegalPersonArea = this.command.dLegalPersonArea.split(",");
            }

            if (this.command.pricePackageId) {
                this.serviceVisible = true;
            }
            this.prices.relocationCost = this.command.apartment.relocationCost;
            this.prices.refundCost = this.command.apartment.refund;
            this.prices.attachCost = this.command.apartment.attachedServiceCost;
            this.prices.liquidatedDamages = this.command.apartment.penaltyPercent + "%";
            this.prices.mealsCost = this.command.pricePackage.priceItems.find(
                x => x.name == "餐费" && x.type == 1
            ).price;
            this.prices.serviceCost = this.command.pricePackage.priceItems.find(
                x => x.name == "服务费" && x.type == 1
            ).price;
            const depositType = this.command.pricePackage.priceItems.find(price => price.id == this.command.depositTypeId);
            console.log(this.command);
            if (depositType) {
                this.depositType = depositType.name;
            }
            const liveType = this.command.pricePackage.priceItems.find(x => x.id == this.command.liveTypeId);

            if (liveType) {
                this.command.serviceMonthlyCost = liveType.price;
                this.command.liveTypeName = liveType.name;
            }
            // console.log(this.command);
            this.apartment = this.command.apartment;
        }
    },

    methods: {
        goBack() {
            this.$router.go(-1);
        }
    }
};
</script>
<style>
</style>
