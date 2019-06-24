<template>
    <section>
        <el-form ref="form" :rules="rules" :model="command" label-width="120px" :disabled="disabled">
            <el-row class="titie">公寓信息</el-row>
            <el-row>
                <el-col :span="8" v-if="user.supperAdmin">
                    <el-form-item label="所属公司">
                        <el-select style="width: 100%;" v-model="command.companyId" placeholder="请选择所属公司">
                            <el-option v-for="item in companies" :key="item.id" :label="item.name" :value="item.id"></el-option>
                        </el-select>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="公寓名称" prop="name">
                        <el-input v-model="command.name" placeholder="请输入公寓名称"></el-input>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="公寓类型" prop="type">
                        <el-select style="width: 100%;" v-model="command.type" placeholder="请选择公寓类型">
                            <el-option label="CC" :value="1"></el-option>
                            <el-option label="CB" :value="2"></el-option>
                            <el-option label="CLRC" :value="3"></el-option>
                        </el-select>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="所属城市" prop="area">
                        <AreaSelector style="width:100%;" v-model="command.area"></AreaSelector>
                        <!-- <el-cascader :options="options" style="width:100%"></el-cascader> -->
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="公寓地点" prop="address">
                        <el-input v-model="command.address" placeholder="请输入公寓地点"></el-input>
                    </el-form-item>
                </el-col>
                <el-col :span="8" v-if="!isEdit">
                    <el-form-item label="楼栋数">
                        <el-input-number v-model="command.buildingCount" controls-position="right" :min="1" :max="120" :precision="0" style="width:100%;" label="请输入楼栋数"></el-input-number>
                    </el-form-item>
                </el-col>
                <el-col :span="8" v-if="!isEdit">
                    <el-form-item label="单元数">
                        <el-input-number v-model="command.unitCount" controls-position="right" :min="1" :max="120" :precision="0" style="width:100%;" label="请输入单元数"></el-input-number>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="楼层数" v-if="!isEdit">
                        <el-input-number v-model="command.floorCount" controls-position="right" :min="1" :max="120" :precision="0" style="width:100%;" label="请输入楼层数"></el-input-number>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="房间数" v-if="!isEdit">
                        <el-input-number v-model="command.roomCount" controls-position="right" :min="1" :max="120" :precision="0" style="width:100%;" label="请输入房间数"></el-input-number>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="床位数" v-if="!isEdit">
                        <el-input-number v-model="command.bedCount" controls-position="right" :min="1" :max="120" :precision="0" label="请输入床位数" style="width:100%;"></el-input-number>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row class="titie">主体信息</el-row>
            <el-row>
                <el-col :span="8">
                    <el-form-item label="机构法人" prop="corporation">
                        <el-input v-model="command.corporation" placeholder="请输入机构法人"></el-input>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="联系电话" prop="phone">
                        <el-input v-model="command.phone" placeholder="请输入联系电话"></el-input>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="电子邮箱" prop="email">
                        <el-input v-model="command.email" placeholder="请输入电子邮箱"></el-input>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="账户名称" prop="accountName">
                        <el-input v-model="command.accountName" placeholder="请输入账户名称"></el-input>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="账户号码" prop="accountNumber">
                        <el-input v-model="command.accountNumber" placeholder="请输入账户号码"></el-input>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="开户行" prop="bankName">
                        <el-input v-model="command.bankName" placeholder="请输入开户行"></el-input>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="邮政编码" prop="postcode">
                        <el-input v-model="command.postcode" placeholder="请输入邮政编码"></el-input>
                    </el-form-item>
                </el-col>
                <!-- <el-col :span="8"><el-form-item label="机构地址" prop="address">
                    <el-input v-model="command.address" placeholder="请输入机构地址"></el-input>
                </el-form-item></el-col>-->
            </el-row>
            <el-row class="titie">其他信息</el-row>
            <el-row>
                <!-- <el-col :span="8">
                    <el-form-item label="入住押金">
                        <el-input-number
                            v-model="command.deposit"
                            controls-position="right"
                            :min="0"
                            :precision="2"
                            label="请输入押金"
                            style="width:100%;"
                        ></el-input-number>
                    </el-form-item>
                </el-col>-->
                <el-col :span="8">
                    <el-form-item label="一次性安置费" prop="relocationCost">
                        <el-input-number v-model="command.relocationCost" controls-position="right" :min="0" :precision="2" label="请输入一次性安置费" style="width:100%;"></el-input-number>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="请假退餐费" prop="refund">
                        <el-input-number v-model="command.refund" controls-position="right" :min="0" :precision="2" label="请输入请假退餐费" style="width:100%;"></el-input-number>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="附加服务费" prop="attachedServiceCost">
                        <el-input-number v-model="command.attachedServiceCost" controls-position="right" :min="0" :precision="2" label="请输入附加服务费" style="width:100%;"></el-input-number>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="推荐人奖励积分" prop="recommendPoint">
                        <el-input-number v-model="command.recommendPoint" controls-position="right" :min="0" :precision="0" label="请输入推荐人奖励积分" style="width:100%;"></el-input-number>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="被推荐人奖励积分" prop="bonusPoint">
                        <el-input-number v-model="command.bonusPoint" controls-position="right" :min="0" :precision="0" label="请输入被推荐人奖励积分" style="width:100%;"></el-input-number>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="违约金比例/%" prop="penaltyPercent">
                        <el-input-number v-model="command.penaltyPercent" controls-position="right" :min="0" :max="100" :precision="0" label="请输入违约金比例" style="width:100%;"></el-input-number>
                    </el-form-item>
                </el-col>
                <!-- <el-col :span="8">
                    <el-form-item label="是否启用">
                        <el-switch v-model="command.enabled"></el-switch>
                    </el-form-item>
                </el-col>-->
            </el-row>
        </el-form>
        <el-row style="margin-top:18px;">
            <el-button type="primary" @click="save('form')" v-if="!disabled">保存</el-button>
            <el-button @click="back">取消</el-button>
        </el-row>
    </section>
</template>
<script>
import ElementUI from "element-ui";
import AreaSelector from "@/components/AreaSelector.vue";
// import {
//     provinceAndCityData,
//     regionData,
//     provinceAndCityDataPlus,
//     regionDataPlus,
//     CodeToText,
//     TextToCode
// } from "element-china-area-data";
import api from "../../../config/api";
import * as types from "../../../store/types";
import validators from "../../../common/validators";

// Vue.use(iviewArea);
export default {
    components: { AreaSelector },
    data() {
        return {
            rules: {
                name: [{ required: true, message: "请输入公寓名称", trigger: "blur" }],
                type: [
                    { required: true, message: "请选择公寓类型", trigger: "change" }
                ],
                area: [
                    { required: true, message: "请选择所属城市", trigger: "change" }
                ],
                address: [
                    { required: true, message: "请选择公寓地点", trigger: "change" }
                ],
                relocationCost: [
                    { required: true, message: "请输入一次性安置费", trigger: "change" }
                ],
                refund: [
                    { required: true, message: "请输入请假退餐费", trigger: "change" }
                ],
                attachedServiceCost: [
                    { required: true, message: "请输入附加服务费", trigger: "change" }
                ],
                recommendPoint: [
                    { required: true, message: "请输入推荐人奖励积分", trigger: "change" }
                ],
                bonusPoint: [
                    {
                        required: true,
                        message: "请输入被推荐人奖励积分",
                        trigger: "change"
                    }
                ],
                penaltyPercent: [
                    { required: true, message: "请输入违约金比例", trigger: "change" }
                ],
                corporation: [
                    { required: true, message: "请输入机构法人", trigger: "blur" }
                ],
                phone: [
                    { required: true, message: "请输入联系电话", trigger: "blur" },
                    { validator: validators.phone, trigger: "blur" }
                ],
                email: [
                    // { required: true, message: "请输入电子邮箱", trigger: "blur" },
                    {
                        type: "email",
                        message: "电子邮箱格式错误",
                        trigger: "blur"
                    }
                ],
                accountName: [
                    { required: true, message: "请输入账户名称", trigger: "blur" }
                ],
                accountNumber: [
                    { required: true, message: "请输入账户号码", trigger: "blur" },
                    { validator: validators.bankAccount, trigger: "blur" }
                ],
                bankName: [
                    { required: true, message: "请输入开户行", trigger: "blur" }
                ],
                postcode: [
                    // { required: true, message: "请输入邮政编码", trigger: "blur" },
                    { validator: validators.zipCode, trigger: "blur" }
                ]
            },
            command: {},
            loading: true,
            companies: [],
            disabled: false,
            user: {},
            isEdit: false
        };
    },

    // Component引入后才可执行该方法
    async mounted() {
        if (this.$route.params.id != undefined) {
            this.command = (await this.$axios.get(
                "apartment/" + this.$route.params.id
            )).data;
            if (this.$route.params.disabled == "true") {
                this.disabled = true;
            }
            this.isEdit = true;
        }
        this.user = this.$store.state.user;
        if (this.user && this.user.superAdmin == false) {
            this.command.companyId = this.user.companyId;
        } else {
            this.companies = (await this.$axios.get("company/select-list")).data;
        }
    },
    methods: {
        save(form) {
            this.$refs.form.validate(valid => {
                if (valid) {
                    this.loading = true;
                    const httpRequest = this.command.id
                        ? this.$axios.put("apartment", this.command)
                        : this.$axios.post("apartment", this.command);
                    httpRequest.then(
                        response => {
                            this.loading = false;
                            this.$message({
                                type: "success",
                                message: "保存成功!"
                            });
                            // this.search();
                            this.back();
                        },
                        error => {
                            this.loading = false;
                            const result = error.response.data;
                            this.$message({
                                type: "error",
                                message: "保存失败:" + result.Message,
                                center: true
                            });
                        }
                    );
                } else {
                    return false;
                }
            });
        },

        back() {
            // this.$refs.form.resetFields();
            // this.command = {};
            this.$router.go(-1);
        }
    }
};
</script>
<style>
</style>
