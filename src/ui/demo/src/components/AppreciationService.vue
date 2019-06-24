<template>
    <section>
        <el-form ref="form" :rules="rules" :model="command" label-width="95px">
            <el-row>
                <el-col :span="8">
                    <el-form-item label="服务项目" prop="appreciationServices">
                        <el-cascader :options="appreciationServices" placeholder="请选择服务项目"
                                     v-model="command.appreciationServices" style="width:100%;"
                                     @change="changeAppreciationServices"></el-cascader>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="服务日期" prop="serviceTimes">
                        <el-date-picker
                                type="dates"
                                v-model="command.serviceTimes"
                                style="width:100%;"
                                placeholder="选择一个或多个服务日期">
                        </el-date-picker>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="服务次数" prop="serviceCount">
                        <el-input-number v-model="command.serviceCount" controls-position="right" :min="1"
                                         :precision="0" style="width:100%;" label="请输入服务次数"
                                         @blur="getPrice"></el-input-number>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="服务价格">
                        {{servicePrice.price}}
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="服务总价">
                        {{servicePrice.total}}
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="服务说明">
                        <!--{{servicePrice.total}}-->
                    </el-form-item>
                </el-col>
                <el-col :span="24">
                    <el-form-item label="备注">
                        <el-input type="textarea" v-model="command.decription"
                                  :autosize="{ minRows: 2, maxRows: 4}" placeholder="请输入备注"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
        </el-form>
        <el-row class="table-row">
            <span style="float:right;margin-bottom: 18px;" v-if="$hasPermission('新增')">
              <el-button type="primary" plain @click.stop="createConsumable" size="mini"
                         icon="el-icon-circle-plus">新增</el-button>
            </span>
            <el-table :data="command.consumables">
                <el-table-column prop="name" label="耗材名称"></el-table-column>
                <el-table-column prop="cost" label="单价"></el-table-column>
                <el-table-column prop="count" label="数量">
                    <template slot-scope="scope">
                        <el-input-number :key="scope.row.id" v-model="scope.row.count" :controls="false"
                                         controls-position="right" :min="1"
                                         :precision="0" style="width:100%;" label="请输入服务次数"
                                         @blur="getPrice"></el-input-number>
                    </template>
                </el-table-column>
                <el-table-column fixed="right" label="操作">
                    <template slot-scope="scope">
                        <el-button @click="del(scope.row)" type="text" v-if="$hasPermission('删除')">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </el-row>
        <div slot="footer" class="dialog-footer" style="margin-top: 18px;">
            <el-button @click="back">取 消</el-button>
            <el-button type="primary" @click="save()">确 定</el-button>
        </div>
        <el-dialog :close-on-click-modal="false" title="耗材信息" :visible.sync="dialogConsumableVisible" width="50%"
                   append-to-body>
            <el-row class="search-row el-form el-form--inline">
                <el-col :span="24">
                    <label>耗材名称：</label>
                    <el-input v-model="query.name" placeholder="耗材名称"></el-input>
                    <el-button type="primary" icon="el-icon-search" @click="search">查询</el-button>
                    <!-- <el-button type="info" plain @click="query={}">重置</el-button> -->
                </el-col>
            </el-row>
            <el-table ref="multipleTable" :data="consumables" @selection-change="handleSelectionChange">
                <el-table-column type="selection" width="55">
                </el-table-column>
                <el-table-column prop="name" label="耗材名称"></el-table-column>
                <el-table-column prop="cost" label="单价"></el-table-column>
            </el-table>
            <div slot="footer" class="dialog-footer">
                <el-button @click="cancelConsumable">取 消</el-button>
                <el-button type="primary" @click="confirmConsumable">确 定</el-button>
            </div>
        </el-dialog>
    </section>
</template>

<script>
    import _ from "lodash";
    // import axios from "../utils/http";
    import api from "../config/api";

    export default {
        props: {
            customerId: {
                type: String,
                default: function () {
                    return "";
                }
            },
            apartmentId: {
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
                query: {},
                dialogConsumableVisible: false,
                command: {},
                appreciationServices: [],
                consumables: [],
                dialogFormVisible: false,
                loading: false,
                status: false, // 给父组件反馈一个刷新table状态
                multipleSelection: [],
                servicePrice: {},
                rules: {
                    appreciationServices: [{
                        required: true,
                        message: "请选择服务项目",
                        trigger: "change"
                    }],
                    serviceTimes: [
                        {
                            required: true,
                            // type: "date",
                            message: "请选择服务日期",
                            trigger: "change"
                        }
                    ],
                    serviceCount: [
                        {
                            required: true,
                            message: "请输入服务次数",
                            trigger: "blur"
                        }
                    ]
                }
            };
        },

        async mounted() {
            this.init();
            this.$watch("visible", newValue => {
                this.dialogFormVisible = newValue;
                if (this.dialogFormVisible) {
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
            //初始化未对dialogFormVisible赋值，因此$watch-"visible"未被激活
            this.dialogFormVisible = true;
        },

        methods: {
            handleSelectionChange(vals) {
                this.multipleSelection = vals;
            },
            del(row) {
                this.command.consumables.splice(this.command.consumables.findIndex(item => item == row), 1)
                this.getPrice();
            },
            async search() {
                this.consumables = (await this.$axios.post(api.consumable + "/select-list", this.query)).data;
            },
            getPrice() {
                let total = 0;
                let price = 0;
                const consumables = this.appreciationServices.map(x => x.children).flat();
                const consumable = consumables.find(x => x.id == this.command.appreciationServiceId);
                // if (consumable) {
                //     total = consumable.price;
                // }
                if (consumable) {
                    price = consumable.price;
                    total = Number(this.command.serviceCount) * price;
                }
                let consumablePrice = 0;
                if (this.command.consumables && this.command.consumables.length > 0) {
                    this.command.consumables.forEach(x => {
                        consumablePrice += x.price * x.count;
                    });
                }
                this.servicePrice = {
                    total: total + consumablePrice,
                    price: price
                };
            },
            confirmConsumable() {
                let consumables = _.clone(this.command.consumables);
                if (consumables && this.multipleSelection) {
                    this.multipleSelection.forEach(x => {
                        let matched = consumables.find(y => y.id == x.id);
                        if (matched) {
                            matched.count += 1;
                        } else {
                            consumables.push({...x, count: 1});
                        }
                    });
                    this.$set(this.command, "consumables", consumables);
                } else {
                    this.multipleSelection.filter(c => {
                        c.count = 1;
                    });
                    this.$set(this.command, "consumables", this.multipleSelection);
                }
                this.getPrice();
                this.cancelConsumable();
            },
            cancelConsumable() {
                this.dialogConsumableVisible = false;
            },
            createConsumable() {
                this.dialogConsumableVisible = true;
                this.multipleSelection = [];
                if (this.$refs.multipleTable) {
                    this.$refs.multipleTable.clearSelection();
                }
            },
            back() {
                this.dialogFormVisible = false;
            },
            changeAppreciationServices(vals) {
                if (vals && vals.length > 0) {
                    this.command.appreciationServiceId = vals[1];
                    this.getPrice();
                }
            },
            async init() {
                this.command.customerId = this.$props.customerId;
                this.command.apartmentId = this.$props.apartmentId;
                this.appreciationServices = (await this.$axios.post(api.appreciationService + "/select-list", {apartmentId: this.command.apartmentId})).data;
                this.query.apartmentId = this.command.apartmentId;
                this.search();
            },
            reset() {
                this.status = false;
            },

            save() {
                this.$refs.form.validate(valid => {
                    if (valid) {
                        this.saving = true;
                        this.$axios
                            .post(api.valueAddedService, this.command)
                            .then(response => {
                                this.saving = false;
                                this.$message({
                                    message: "保存成功",
                                    type: "success"
                                });
                                this.back();
                            })
                            .catch(err => {
                                this.saving = false;
                                this.$message({
                                    message: "保存失败: " + err.message,
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
