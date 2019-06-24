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
                                <ConfigurationSelector
                                        v-model="command.consultingCustomerType"
                                        type="销售项配置"
                                        name="客户类型"
                                        style="width:100%"
                                ></ConfigurationSelector>
                            </el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="身体状况" prop="health">
                                <ConfigurationSelector
                                        v-model="command.health"
                                        type="销售项配置"
                                        name="身体状况"
                                        style="width:100%"
                                ></ConfigurationSelector>
                            </el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="工作行业" prop="industry">
                                <ConfigurationSelector
                                        v-model="command.industry"
                                        type="销售项配置"
                                        name="工作行业"
                                        style="width:100%"
                                ></ConfigurationSelector>
                            </el-form-item>
                        </el-col>
                    </el-row>
                    <el-row>
                        <el-col :span="8">
                            <el-form-item label="获知渠道">
                                <ConfigurationSelector
                                        v-model="command.whereToKnow"
                                        type="销售项配置"
                                        name="获知渠道"
                                        style="width:100%"
                                ></ConfigurationSelector>
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
                            <el-form-item label="意向公寓" prop="apartmentId">
                                <el-select
                                        v-if="apartmentList"
                                        v-model="command.apartmentId"
                                        @change="apartmentChanged"
                                        style="width:100%"
                                >
                                    <el-option
                                            v-for="item in apartmentList"
                                            :key="item.value"
                                            :label="item.text"
                                            :value="item.value"
                                    >{{item.text}}
                                    </el-option>
                                </el-select>
                            </el-form-item>
                        </el-col>
                    </el-row>
                    <el-row>
                        <el-col :span="8">
                            <el-form-item label="意向房型" prop="roomTypeId">
                                <el-select v-model="command.roomTypeId" style="width:100%">
                                    <el-option
                                            v-for="item in roomTypeList"
                                            :key="item.value"
                                            :label="item.text"
                                            :value="item.value"
                                    >{{item.text}}
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
                            <el-input-number
                                    v-model="command.age"
                                    controls-position="right"
                                    :min="1"
                                    :max="120"
                                    :precision="0"
                                    label="请输入年龄"
                            ></el-input-number>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="联系电话" prop="phone">
                            <el-input v-model="command.phone" placeholder="请输入客户姓名"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="居住区域">
                            <AreaSelector v-model="area" style="width:100%;" :value="area"></AreaSelector>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="联系地址">
                            <el-input v-model="command.address" placeholder="请输入客户姓名"></el-input>
                        </el-form-item>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="推荐人信息" name="3">
                    <el-col :span="8">
                        <el-form-item label="推荐类型">
                            <ConfigurationSelector
                                    v-model="command.referralType"
                                    type="销售项配置"
                                    name="推荐类型"
                                    style="width:100%;"
                            ></ConfigurationSelector>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="推荐渠道">
                            <ConfigurationSelector
                                    v-model="command.referralChannel"
                                    type="销售项配置"
                                    name="推荐渠道"
                                    style="width:100%;"
                            ></ConfigurationSelector>
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
                            <ConfigurationSelector
                                    v-model="command.consultantRelation"
                                    type="销售项配置"
                                    name="与客户关系"
                                    style="width:100%;"
                            ></ConfigurationSelector>
                        </el-form-item>
                    </el-col>
                    <el-col :span="24">
                        <el-form-item label="备注">
                            <el-input type="textarea" v-model="command.consultantRemark" :autosize="{ minRows: 2, maxRows: 4}" placeholder="请输入备注"
                            ></el-input>
                        </el-form-item>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item name="5">
                    <template slot="title">
                        客户跟踪列表
                        <span style="float:right;" v-if="$hasPermission('新增')">
              <el-button type="primary" plain @click.stop="createForm" size="mini" icon="el-icon-circle-plus">新增</el-button>
            </span>
                    </template>
                    <el-col :span="24">
                        <el-table :data="command.traces" style="width: 100%;padding:auto 10px;">
                            <el-table-column label="序号">
                                <template slot-scope="scope">{{scope.$index + 1}}</template>
                            </el-table-column>
                            <el-table-column label="所属公寓">
                                <template slot-scope="scope">{{command.apartmentName}}</template>
                            </el-table-column>
                            <el-table-column prop="typeText" label="沟通类型"></el-table-column>
                            <el-table-column label="开始日期">
                                <template slot-scope="scope">{{moment(scope.row.startTime).format("YYYY-MM-DD")}}
                                </template>
                            </el-table-column>
                            <el-table-column label="结束日期">
                                <template slot-scope="scope">{{moment(scope.row.endTime).format("YYYY-MM-DD")}}
                                </template>
                            </el-table-column>
                            <el-table-column prop="seller" label="销售人员">
                                <template slot-scope="scope">{{command.sellerName}}</template>
                            </el-table-column>
                            <el-table-column prop="record" label="沟通记录"></el-table-column>
                            <el-table-column label="操作">
                                <template slot-scope="scope">
                                    <el-button
                                            @click="deleteTrace(scope.row.id)"
                                            type="text"
                                            v-if="$hasPermission('新增')"
                                    >删除
                                    </el-button>
                                </template>
                            </el-table-column>
                        </el-table>
                    </el-col>
                </el-collapse-item>
            </el-collapse>
        </el-form>

        <el-dialog
                :close-on-click-modal="false"
                title="跟踪信息"
                :visible.sync="dialogFormVisible"
                width="30%"
        >
            <el-form ref="dialogForm" :model="dialogForm" :rules="traceRules" label-width="80px">
                <el-form-item label="沟通类型" prop="type">
                    <ConfigurationSelector
                            v-model="dialogForm.type"
                            type="销售项配置"
                            name="追踪类型"
                            style="width:100%"
                    ></ConfigurationSelector>
                </el-form-item>
                <!--<el-form-item label="所属公寓">-->
                <!--<el-select v-model="dialogForm.b" placeholder="请选择所属公寓">-->
                <!--<el-option label="养老服务管理" value="0"></el-option>-->
                <!--<el-option label="养老服务管理1" value="1"></el-option>-->
                <!--</el-select>-->
                <!--</el-form-item>-->
                <el-form-item label="开始日期" prop="startTime">
                    <el-date-picker
                            v-model="dialogForm.startTime"
                            type="date"
                            placeholder="选择日期"
                            style="width:100%"
                    ></el-date-picker>
                </el-form-item>
                <el-form-item label="完成日期" prop="endTime">
                    <el-date-picker
                            v-model="dialogForm.endTime"
                            type="date"
                            placeholder="选择日期"
                            style="width:100%"
                    ></el-date-picker>
                </el-form-item>
                <el-form-item label="跟踪记录" prop="record">
                    <el-input v-model="dialogForm.record" type="textarea" placeholder="请输入跟踪记录"></el-input>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button :loading="traceSaving" type="primary" @click="saveTrace">确 定</el-button>
                <el-button @click="cancel">取 消</el-button>
            </div>
        </el-dialog>
        <el-row class="button-bottom-row">
            <el-button type="primary" :loading="saving" @click="save">保存</el-button>
            <el-button @click="goBack">取消</el-button>
        </el-row>
    </section>
</template>
<script>
    import ConfigurationSelector from "../../components/ConfigurationSelector.vue";
    import AreaSelector from "../../components/AreaSelector.vue";
    import api from "../../config/api";
    import isChinesePhoneNumber from "is-chinese-phone-number";
    import moment from "moment";
    import ElementUI from "element-ui";
    import _ from "lodash";
    import {
        provinceAndCityData,
        regionData,
        provinceAndCityDataPlus,
        regionDataPlus,
        CodeToText,
        TextToCode
    } from "element-china-area-data";

    export default {
        components: {
            ConfigurationSelector,
            AreaSelector
        },
        data() {
            return {
                disabledType: false,
                activeNames: ["1", "2", "3", "4", "5"],
                options: regionData,
                area: [],
                consultantArea: [],
                selectedOptions: [],
                dialogForm: {
                    consultingId: "",
                    id: ""
                },
                dialogFormVisible: false,
                saving: false,
                traceSaving: false,
                command: {
                    id: "",
                    area: "",
                    consultantArea: "",
                    traces: [],
                    consultingType: null,
                    consultantName: "",
                    consultantPhone: "",
                    consultantRelation: null
                },
                consultingStatusList:[],
                apartmentList: [],
                roomTypeList: [],
                traceRules: {
                    type: [
                        {
                            required: true,
                            message: "请选择跟踪方式",
                            trigger: "change"
                        }
                    ],
                    startTime: [
                        {
                            required: true,
                            message: "请选择开始日期",
                            trigger: "change"
                        }
                    ],
                    endTime: [
                        {
                            required: true,
                            message: "请选择结束日期",
                            trigger: "change"
                        }
                    ],
                    record: [
                        {
                            required: true,
                            message: "请输入跟踪记录",
                            trigger: "blur"
                        }
                    ]
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
                    apartmentId: [
                        {
                            validator: this.validatePurposeApartment,
                            trigger: "change"
                        }
                    ],
                    roomTypeId: [
                        {
                            validator: this.validatePurposeRoomType,
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
            validatePurposeApartment(rule, value, callback) {
                if (!this.command.apartmentId) {
                    callback(new Error("请选择意向公寓"));
                } else {
                    callback();
                }
            },
            validatePurposeRoomType(rule, value, callback) {
                if (!this.command.roomTypeId) {
                    callback(new Error("请选择意向房型"));
                } else {
                    callback();
                }
            },
            typeChange(val) {
                this.disabledType = val === 2 ? true : false;
            },
            async search() {
                this.apartmentChanged(this.command.apartmentId, true);
            },
            async deleteTrace(id) {
                this.$confirm("此操作将永久删除此追踪记录, 是否继续?", "提示", {
                    confirmButtonText: "确定",
                    cancelButtonText: "取消",
                    type: "warning"
                })
                    .then(() => {
                        this.$axios
                            .delete(`${api.deleteTrace}/${id}`)
                            .then(response => {
                                this.$message({
                                    type: "success",
                                    message: "删除成功!"
                                });
                                let index = _.findIndex(this.command.traces, x => x.id == id);
                                this.command.traces.splice(index, 1);
                            })
                            .catch(err => {
                                this.$message({
                                    type: "error",
                                    message: err.message
                                });
                            });
                    })
                    .catch(() => {
                        1;
                        // this.$message({
                        //     type: 'info',
                        //     message: '已取消删除'
                        // });
                    });
            },
            async saveTrace() {
                this.$refs.dialogForm.validate(async valid => {
                    if (valid) {
                        console.log(this.dialogForm);
                        try {
                            this.dialogForm.consultingId = this.command.id;
                            this.traceSaving = true;
                            if (this.dialogForm.id) {
                                await this.$axios.put("consulting/trace", this.dialogForm);
                            } else {
                                await this.$axios.post("consulting/trace", this.dialogForm);
                            }
                            this.traceSaving = false;
                            this.$message({
                                message: "保存成功",
                                type: "success"
                            });
                            this.dialogFormVisible = false;
                            this.$refs.dialogForm.resetFields();
                            this.$axios.get(`consulting/${this.command.id}/traces`).then(response => {
                                this.command.traces = response.data;
                            }).catch(err => {
                                this.traceSaving = false;
                                console.log(err);
                            })
                        } catch (error) {
                            this.traceSaving = false;
                            console.log(error);
                        }
                    } else {
                        console.log("error submit!!");
                        return false;
                    }
                });
            },
            save() {
                this.$refs.form.validate(async valid => {
                    if (valid) {
                        if(this.command.phone == this.command.referrerPhone)
                        {
                            this.$message({
                                message: "客户联系方式和推荐人联系方式不能重复",
                                type: "error"
                            });
                            return;
                        }
                        try {
                            this.saving = true;
                            this.command.area = this.area.join(",");
                            if (this.consultantArea.length > 0) {
                                this.command.consultantArea = this.consultantArea.join(",");
                            }
                            await this.$axios.put("consulting", this.command);
                            this.saving = false;
                            this.$message({
                                message: "保存成功",
                                type: "success"
                            });
                            this.$router.replace({
                                name: "consulting"
                            });
                        } catch (e) {
                            this.saving = false;
                            console.log(e);
                        }
                    }
                })
            },
            createForm() {
                this.dialogFormVisible = true;
                if (this.$refs["dialogForm"]) {
                    this.$refs["dialogForm"].clearValidate(); // 重置验证
                }
            },
            cancel() {
                this.dialogForm = {consultingId: "", id: ""};
                this.dialogFormVisible = false;
            },
            goBack() {
                this.$router.go(-1);
            },
            apartmentChanged(id, first) {
                this.$axios
                    .get(api.roomTypeDropdown.replace("{id}", id))
                    .then(response => {
                        this.roomTypeList = response.data.map(x => {
                            return {
                                text: x.name,
                                value: x.id
                            };
                        });
                        if (!first) {
                            this.$set(this.command, "roomTypeId", undefined);
                        }
                    })
                    .catch(err => {
                        console.log(err.message);
                    });
            }
        },
        async mounted() {
            let data = (await this.$axios.get("consulting/" + this.$route.params.id))
                .data;
            this.command = _.clone(data);
            console.log(this.command);
            this.$axios.get(api.apartmentDropdown).then(response => {
                this.apartmentList = response.data.map(x => {
                    return {
                        text: x.name,
                        value: x.id
                    };
                });
                this.search();
            }).catch(err => {
                console.log(err.message);
            });
            this.$axios.get(api.consultingStatus).then(response => {
                this.consultingStatusList = response.data;
            }).catch(err => {
                console.log(err.message);
            });
            console.log(this.command.area);
            this.area = this.command.area ? this.command.area.split(",") : [];
            this.consultantArea = this.command.consultantArea
                ? this.command.consultantArea.split(",")
                : [];
            this.typeChange(this.command.consultingType);
            if (this.$refs["form"]) {
                this.$refs["form"].clearValidate(); // 重置验证
            }
        }
    };
</script>
<style>
</style>
