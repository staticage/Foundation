<template>
    <section style="margin-top:-10px;">
        <el-form ref="form" :model="command" :rules="rules" label-width="80px">
            <el-collapse v-model="activeNames">
                <el-collapse-item title="客户基础信息" name="1">
                    <el-row>
                        <el-col :span="8">
                            <el-form-item label="咨询类型" prop="consultingType">
                                <el-radio-group v-model="command.consultingType" @change="typeChange">
                                    <el-radio :label="1">本人咨询</el-radio>
                                    <el-radio :label="2">为长辈咨询</el-radio>
                                </el-radio-group>
                            </el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="入住需求" prop="isCompartment">
                                <el-radio-group v-model="command.isCompartment">
                                    <el-radio :label="false">拼房</el-radio>
                                    <el-radio :label="true">包房</el-radio>
                                </el-radio-group>
                            </el-form-item>
                        </el-col>
                    </el-row>
                    <el-row>
                        <el-col :span="8">
                            <el-form-item label="客户类型" prop="consultingCustomerType">
                                <ConfigurationSelector v-model="command.consultingCustomerType" type="销售项配置" name="客户类型"
                                                       style="width:100%;"></ConfigurationSelector>
                            </el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="身体状况" prop="health">
                                <ConfigurationSelector v-model="command.health" type="销售项配置" name="身体状况"
                                                       style="width:100%;"></ConfigurationSelector>
                            </el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="工作行业" prop="industry">
                                <ConfigurationSelector v-model="command.industry" type="销售项配置" name="工作行业"
                                                       style="width:100%;"></ConfigurationSelector>
                            </el-form-item>
                        </el-col>
                    </el-row>
                    <el-row>
                        <el-col :span="8">
                            <el-form-item label="获知渠道" prop="whereToKnow">
                                <ConfigurationSelector v-model="command.whereToKnow" type="销售项配置" name="获知渠道"
                                                       style="width:100%;"></ConfigurationSelector>
                            </el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="状态" prop="consultingStatus">
                                <el-select v-if="consultingStatusList" v-model="command.consultingStatus"
                                           style="width:100%;">
                                    <el-option v-for="item in consultingStatusList" :key="item.value" :label="item.name"
                                               :value="item.value">{{item.name}}
                                    </el-option>
                                </el-select>
                            </el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="意向公寓" prop="purposeApartment">
                                <el-select v-if="apartmentList" v-model="command.purposeApartment"
                                           @change="apartmentChanged" style="width:100%;">
                                    <el-option v-for="item in apartmentList" :key="item.value" :label="item.text"
                                               :value="item.value">{{item.text}}
                                    </el-option>
                                </el-select>
                            </el-form-item>
                        </el-col>
                    </el-row>
                    <el-row>
                        <el-col :span="8">
                            <el-form-item label="意向房型" prop="purposeRoomType" ref="formRoomType">
                                <el-select v-model="command.purposeRoomType" style="width:100%;">
                                    <el-option v-for="item in roomTypeList" :key="item.value" :label="item.text"
                                               :value="item.value">{{item.text}}
                                    </el-option>
                                </el-select>
                            </el-form-item>
                        </el-col>
                    </el-row>
                </el-collapse-item>
                <el-collapse-item title="客户其他信息" name="2">
                    <el-col :span="8">
                        <el-form-item label="客户姓名" prop="name">
                            <el-input v-model="command.name" placeholder="请输入客户姓名"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="4">
                        <el-form-item label="性别" prop="sex">
                            <el-radio-group v-model="command.sex">
                                <el-radio :label="1">男</el-radio>
                                <el-radio :label="2">女</el-radio>
                            </el-radio-group>
                        </el-form-item>
                    </el-col>
                    <el-col :span="4">
                        <el-form-item label="年龄">
                            <el-input-number v-model="command.age" controls-position="right" :min="1" :max="120"
                                             :precision="0" label="请输入年龄" style="width:100%;"></el-input-number>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="联系电话" prop="phone">
                            <el-input v-model="command.phone" placeholder="请输入联系电话"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="居住区域">
                            <AreaSelector v-model="area" style="width:100%;"></AreaSelector>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="联系地址">
                            <el-input v-model="command.address" placeholder="请输入联系地址"></el-input>
                        </el-form-item>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="推荐人信息" name="3">
                    <el-col :span="8">
                        <el-form-item label="推荐类型">
                            <ConfigurationSelector v-model="command.referralType" type="销售项配置" name="推荐类型"
                                                   style="width:100%;"></ConfigurationSelector>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="推荐渠道">
                            <ConfigurationSelector v-model="command.referralChannel" type="销售项配置" name="推荐渠道"
                                                   style="width:100%;"></ConfigurationSelector>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="推荐人">
                            <el-input v-model="command.referrerName" placeholder="请输入推荐人"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="联系方式" prop="referrerPhone">
                            <el-input v-model="command.referrerPhone" placeholder="请输入联系方式"></el-input>
                        </el-form-item>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="咨询人信息" name="4" v-if="disabledType">
                    <el-col :span="8">
                        <el-form-item label="姓名" prop="consultantName">
                            <el-input v-model="command.consultantName" placeholder="请输入姓名"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="联系电话" prop="consultantPhone">
                            <el-input v-model="command.consultantPhone" placeholder="请输入联系方式"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="居住区域" prop="consultantArea">
                            <AreaSelector v-model="consultantArea" style="width:100%;"></AreaSelector>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="联系地址">
                            <el-input v-model="command.consultantAddress" placeholder="请输入联系地址"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="关系" prop="consultantRelation">
                            <ConfigurationSelector v-model="command.consultantRelation" type="销售项配置" name="与客户关系"
                                                   style="width:100%;"></ConfigurationSelector>
                        </el-form-item>
                    </el-col>
                    <el-col :span="24">
                        <el-form-item label="备注">
                            <el-input type="textarea" v-model="command.consultantRemark"
                                      :autosize="{ minRows: 2, maxRows: 4}" placeholder="请输入备注"></el-input>
                        </el-form-item>
                    </el-col>
                </el-collapse-item>
            </el-collapse>
        </el-form>
        <el-row class="button-bottom-row">
            <el-button type="primary" :loading="saving" @click="create">立即创建</el-button>
            <el-button @click="goBack">取消</el-button>
        </el-row>
    </section>
</template>
<script>
    import ConfigurationSelector from "../../components/ConfigurationSelector.vue";
    import AreaSelector from "../../components/AreaSelector.vue";
    import api from "../../config/api";
    import isChinesePhoneNumber from "is-chinese-phone-number";

    export default {
        components: {
            ConfigurationSelector,
            AreaSelector
        },
        data() {
            return {
                disabledType: false,
                activeNames: ["1", "2", "3", "4"],
                form: {
                    name: "",
                    region: "",
                    date1: "",
                    date2: "",
                    delivery: false,
                    type: [],
                    resource: "",
                    desc: "",
                    age: 0
                },
                apartmentList: null,
                consultingStatusList: null,
                roomTypeList: null,
                area: [],
                saving: false,
                consultantArea: [],
                command: {
                    sellerId: this.$store.state.user.id,
                    area: "",
                    consultantArea: "",
                    age: 1,
                    consultingType: null,
                    consultantName: "",
                    consultantPhone: "",
                    consultantRelation: null
                },
                rules: {
                    name: [
                        {
                            required: true,
                            message: "请输入姓名",
                            trigger: "blur"
                        }
                    ],
                    consultingStatus: [
                        {
                            required: true,
                            message: "请选择状态",
                            trigger: "change"
                        }
                    ],
                    consultingType: [
                        {
                            required: true,
                            message: "请选择咨询类型",
                            trigger: "change"
                        }
                    ],
                    consultingCustomerType: [
                        {
                            required: true,
                            message: "请选择客户类型",
                            trigger: "change"
                        }
                    ],
                    health: [
                        {
                            required: true,
                            message: "请选择身体状况",
                            trigger: "change"
                        }
                    ],
                    purposeApartment: [
                        {
                            required: true,
                            message: "请选择意向公寓",
                            trigger: "change"
                        }
                    ],
                    purposeRoomType: [
                        {
                            required: true,
                            message: "请选择意向房型",
                            trigger: "change"
                        }
                    ],
                    industry: [
                        {
                            required: true,
                            message: "请选择工作行业",
                            trigger: "change"
                        }
                    ],
                    isCompartment: [
                        {
                            required: true,
                            message: "请选择入住需求",
                            trigger: "change"
                        }
                    ],
                    sex: [
                        {
                            required: true,
                            message: "请选择客户性别",
                            trigger: "change"
                        }
                    ],
                    phone: [
                        {validator: this.validatePhone, trigger: "blur"}
                    ],
                    referrerPhone: [
                        {validator: this.validateReferrerPhone, trigger: "submit"}
                    ],
                    consultantName: [
                        {validator: this.validateConsultantName, trigger: "blur"}
                    ],
                    consultantPhone: [
                        {validator: this.validateConsultantPhone, trigger: "blur"}
                    ],
                    consultantArea: [
                        {
                            validator: this.validateConsultantArea,
                            trigger: "change"
                        }
                    ],
                    consultantRelation: [
                        {
                            validator: this.validateConsultantRelation,
                            trigger: "change"
                        }
                    ],
                    whereToKnow: [
                        {
                            required: true,
                            validator: this.validateWhereToKnow,
                            trigger: "change"
                        }
                    ]
                }
            };
        },

        mounted() {
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
            this.$axios.get(api.consultingStatus).then(response => {
                this.consultingStatusList = response.data;
            }).catch(err => {
                console.log(err.message);
            });
        },

        methods: {
            validateConsultantName(rule, value, callback) {
                if (this.command.consultingType === 2) {
                    if (!this.command.consultantName) {
                        callback(new Error("请填写咨询人姓名"));
                    } else {
                        callback();
                    }
                } else {
                    callback();
                }
            },
            validateConsultantPhone(rule, value, callback) {
                if (this.command.consultingType === 2) {
                    if (!this.command.consultantPhone) {
                        callback(new Error("请填写咨询人联系方式"));
                    } else if (!isChinesePhoneNumber(this.command.consultantPhone)) {
                        callback(new Error("电话号码格式有误"));
                    } else {
                        callback();
                    }
                } else {
                    callback();
                }
            },
            validatePhone(rule, value, callback) {
                if (!this.command.phone) {
                    callback(new Error("请填写联系电话"));
                } else if (!isChinesePhoneNumber(this.command.phone)) {
                    callback(new Error("电话号码格式有误"));
                } else {
                    callback();
                }
            },
            validateReferrerPhone(rule, value, callback) {
                if (this.command.referrerPhone && !isChinesePhoneNumber(this.command.referrerPhone)) {
                    callback(new Error("电话号码格式有误"));
                } else {
                    callback();
                }
            },
            validateConsultantArea(rule, value, callback) {
                if (this.command.consultingType === 2) {
                    if (this.consultantArea.length === 0) {
                        callback(new Error("请填写咨询人区域"));
                    } else {
                        callback();
                    }
                } else {
                    callback();
                }
            },

            validateWhereToKnow(rule, value, callback) {
                if (!this.command.whereToKnow) {
                    callback(new Error("请选择获知渠道"));
                } else {
                    callback();
                }
            },
            validateConsultantRelation(rule, value, callback) {
                if (this.command.consultingType === 2) {
                    if (!this.command.consultantRelation) {
                        callback(new Error("请选择关系"));
                    } else {
                        callback();
                    }
                } else {
                    callback();
                }
            },

            typeChange(val) {
                this.disabledType = val === 2 ? true : false;
            },

            create() {
                this.$refs.form.validate(valid => {
                    if (valid) {
                        if (this.command.phone == this.command.referrerPhone) {
                            this.$message({
                                message: "客户联系方式和推荐人联系方式不能重复",
                                type: "error"
                            });
                            return;
                        }
                        this.saving = true;
                        this.command.area = this.area.join(",");
                        if (this.consultantArea.length > 0) {
                            this.command.consultantArea = this.consultantArea.join(",");
                        }
                        this.$axios
                            .post("consulting", this.command)
                            .then(response => {
                                this.saving = false;
                                this.$message({
                                    message: "保存成功",
                                    type: "success"
                                });
                                this.$router.push("/sales");
                            })
                            .catch(err => {
                                this.saving = false;
                                this.$message({
                                    message: "添加失败："+err.message,
                                    type: "error"
                                });
                            });
                    } else {
                        return false;
                    }
                });
            },

            goBack() {
                this.$router.go(-1);
            },

            apartmentChanged(id) {
                this.$axios
                    .get(api.roomTypeDropdown.replace("{id}", id))
                    .then(response => {
                        this.roomTypeList = response.data.map(x => {
                            return {
                                text: x.name,
                                value: x.id
                            };
                        });
                        this.$set(this.command, "purposeRoomType", undefined);
                    })
                    .catch(err => {
                        console.log(err.message);
                    });
            }
        }
    };
</script>
<style>
</style>
