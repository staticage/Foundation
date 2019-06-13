<template>
    <el-dialog :close-on-click-modal="false" title="服务包协议信息" :visible.sync="dialogFormVisible" width="40%">
        <el-form ref="form" :rules="rules" :model="command" label-width="95px">
            <el-form-item label="变更日期" prop="changeDate">
                <el-date-picker v-model="command.changeDate" type="date" placeholder="选择日期"
                                style="width:100%"></el-date-picker>
            </el-form-item>
            <el-form-item label="生活能力类型" prop="liveTypeId">
                <el-select style="width:100%;" placeholder="请选择生活能力类型" v-model="command.liveTypeId"
                           @change="changeLiveType">
                    <el-option v-for="item in liveTypes" :key="item.id" :label="item.name"
                               :value="item.id"></el-option>
                </el-select>
            </el-form-item>
            <el-form-item label="生活能力费用">{{command.serviceMonthlyCost}}</el-form-item>
            <el-form-item label="附加服务费" prop="attachCost">
                <el-input-number v-model="command.attachCost" controls-position="right" :min="0" :precision="2"
                                 label="请输入附加服务费" style="width:100%;"></el-input-number>
            </el-form-item>
        </el-form>
        <div slot="footer" class="dialog-footer">
            <el-button @click="back">取 消</el-button>
            <el-button type="primary" @click="save()">确 定</el-button>
        </div>
    </el-dialog>
</template>

<script>
    import _ from "lodash";
    // import axios from "../utils/http";
    import api from "../config/api";

    export default {
        props: {
            id: {
                type: String,
                default: function () {
                    return "";
                }
            },
            visible: {
                type: Boolean,
                default: function () {
                    return false;
                }
            }
        },

        data() {
            return {
                command: {},
                liveTypes: [],
                dialogFormVisible: false,
                loading: false,
                status: false, // 给父组件反馈一个刷新table状态
                contract: undefined,
                pricePackage: {},
                rules: {
                    changeDate: [{
                        type: "date",
                        required: true,
                        message: "请选择变更日期",
                        trigger: "change"
                    }],
                    liveTypeId: [
                        {
                            required: true,
                            message: "请选择生活能力类型",
                            trigger: "change"
                        }
                    ],
                    attachCost: [
                        {
                            required: true,
                            message: "请输入附加服务费",
                            trigger: "blur"
                        }
                    ]
                }
            };
        },

        mounted() {
            this.$watch("visible", newValue => {
                this.dialogFormVisible = newValue;
                if (this.dialogFormVisible) {
                    if (!this.contract) {
                        this.init();
                    }
                    this.command = {};
                    if (this.$refs["form"]) {
                        this.$refs["form"].clearValidate();
                    }
                }
            });
            this.$watch("dialogFormVisible", newValue => {
                this.dialogFormVisible = newValue;
                if (newValue === false) {
                    this.$emit("callback", {
                        visible: this.dialogFormVisible,
                        status: this.status
                    });
                    this.reset();
                } else {
                }
            });
        },

        methods: {
            back() {
                this.dialogFormVisible = false;
            },
            changeLiveType(liveTypeId) {
                const command = _.cloneDeep(this.command);
                const liveType = this.liveTypes.find(x => x.id == liveTypeId);
                command.serviceMonthlyCost = liveType.price;
                command.liveType = liveType.name;
                this.command = command;
            },
            async init() {
                this.contract = (await this.$axios.get(
                    api.contract + "/" + this.$props.id
                )).data;
                this.pricePackage = _.cloneDeep(this.contract.pricePackage);
                this.liveTypes = this.pricePackage.priceItems.filter(
                    x => x.type == 2
                );
            },
            reset() {
                this.status = false;
            },

            save() {
                this.$refs.form.validate(valid => {
                    if (valid) {
                        this.saving = true;
                        let command = _.cloneDeep(this.command);
                        command.contractId = this.$props.id;
                        this.$axios
                            .post(api.contract + "/service-pack-change", command)
                            .then(response => {
                                this.saving = false;
                                this.$message({
                                    message: "变更成功",
                                    type: "success"
                                });
                                this.back();
                            })
                            .catch(err => {
                                this.saving = false;
                                this.$message({
                                    message: "变更失败: " + err.message,
                                    type: "error"
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

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
    @import "../element-variables";

    .moreBtn {
        display: table;
        white-space: nowrap;
        border-spacing: 0px 0;
        font-size: 12px;
        color: $--color-primary;
        // margin-bottom: 18px;
    }

    .moreBtn:before,
    .moreBtn:after {
        display: table-cell;
        content: "";
        width: 50%;
        background: -webkit-linear-gradient(#eee, #eee) repeat-x left center;
        background: linear-gradient(#eee, #eee) repeat-x left center;
        background-size: 1px 1px;
    }

    .moreBtn sapn {
        cursor: pointer;
    }
</style>
