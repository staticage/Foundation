<template>
    <el-dialog :close-on-click-modal="false" title="换房协议信息" :visible.sync="dialogFormVisible" width="40%">
        <el-form ref="form" :rules="rules" :model="command" label-width="75px">
            <el-form-item label="变更日期" prop="changeDate">
                <el-date-picker v-model="command.changeDate" type="date" placeholder="选择日期"
                                style="width:100%"></el-date-picker>
            </el-form-item>
            <el-form-item label="是否包房" prop="isCompartment">
                <el-radio-group v-model="command.isCompartment" @change="changeIsCompartment">
                    <el-radio :label="false">拼房</el-radio>
                    <el-radio :label="true">包房</el-radio>
                </el-radio-group>
            </el-form-item>
            <el-form-item label="床位" prop="buildings">
                <el-cascader :options="apartment.buildings" v-model="command.buildings" style="width:100%;"
                             @change="changeBuilding"></el-cascader>
            </el-form-item>
            <el-form-item label="房型">{{command.roomType}}</el-form-item>
            <!-- <el-form-item label="收费类型">{{command.serviceMonthlyCost}}</el-form-item> -->
            <el-form-item label="床位费">{{command.roomCost}}</el-form-item>
            <el-form-item label="餐费">{{command.mealsCost}}</el-form-item>
            <el-form-item label="基础服务费">{{command.serviceCost}}</el-form-item>
        </el-form>
        <div slot="footer" class="dialog-footer">
            <el-button @click="back">取 消</el-button>
            <el-button :loading="saving" type="primary" @click="save()">确 定</el-button>
        </div>
    </el-dialog>
</template>

<script>
    import _ from "lodash";
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
                selectOption: {},
                dialogFormVisible: false,
                loading: false,
                saving: false,
                status: false, // 给父组件反馈一个刷新table状态
                contract: undefined,
                pricePackage: {},
                rooms: undefined,
                apartment: {
                    buildings: []
                },
                rules: {
                    changeDate: [{
                        type: "date",
                        required: true,
                        message: "请选择变更日期",
                        trigger: "change"
                    }],
                    isCompartment: [
                        {
                            required: true,
                            message: "请选择是否包房",
                            trigger: "change"
                        }
                    ],
                    buildings: [
                        {required: true, message: "请选择床位", trigger: "change"}
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
                this.command = {};
                if (this.$refs["form"]) {
                    this.$refs["form"].clearValidate();
                }
                this.dialogFormVisible = false;
            },
            changeBuilding(vals) {
                this.getRoomPrice();
            },
            async getRoomPrice() {
                const command = _.cloneDeep(this.command);
                let floorId = command.buildings[2];
                let roomId = command.buildings[3];
                if (roomId && floorId) {
                    if (this.rooms == undefined) {
                        this.rooms = (await this.$axios.get(
                            "room/select-list/" + floorId
                        )).data;
                    }
                    const room = this.rooms.find(room => room.id === roomId);
                    if (room) {
                        command.pricePackageId = this.contract.pricePackageId;
                        command.roomType = room.roomTypeName;
                        if (command.pricePackageId) {
                            let pricePackage = this.pricePackages.find(
                                x => x.id == command.pricePackageId
                            );
                            let roomPrice = pricePackage.priceItems.find(
                                x => x.name == room.roomTypeName && x.type == 0
                            );
                            command.mealsCost = pricePackage.priceItems.find(
                                x => x.name == "餐费" && x.type == 1
                            ).price;
                            command.serviceCost = pricePackage.priceItems.find(
                                x => x.name == "服务费" && x.type == 1
                            ).price;
                            if (roomPrice) {
                                let room;
                                if (this.apartment.buildings.length > 0) {
                                    this.apartment.buildings.filter(b => {
                                        b.units.filter(u => {
                                            u.floors.filter(f => {
                                                f.rooms
                                                    .filter(r => {
                                                        if (r.id == roomId) {
                                                            room = r;
                                                        }
                                                    })
                                            })
                                        })
                                    });
                                }
                                if (room && this.command.isCompartment) {
                                    command.roomCost = roomPrice.price * room.beds.length;// 床位数
                                } else {
                                    command.roomCost = roomPrice.price;
                                }
                            }
                        }
                    }
                } else {
                    command.roomType = undefined;
                    command.roomCost = undefined;
                    command.mealsCost = undefined;
                    command.serviceCost = undefined;
                }
                this.command = command;
            },
            changeIsCompartment(val) {
                // 重置房间信息
                this.command.buildings = [];
                this.command.roomType = "";
                this.command.serviceMonthlyCost = "";
                this.command.roomCost = "";
                this.command.mealsCost = "";
                this.command.serviceCost = "";
            },
            async getBuilding(apartmentId) {
                const buildings = (await this.$axios.get(
                    "apartment/building/" + apartmentId
                )).data.buildings;
                let newBuildings;
                if (this.command.isCompartment == true) {
                    newBuildings = buildings.map(b => {
                        return {
                            ...b,
                            children: b.units.map(u => {
                                return {
                                    ...u,
                                    children: u.floors.map(f => {
                                        return {
                                            ...f,
                                            children: f.rooms
                                                .filter(r => r.enabled && r.roomStatus != 3)
                                                .filter(r => {
                                                    const beds = r.beds.filter(b => b.enabled && !b.occupy);
                                                    if (beds.length == r.beds.length) {
                                                        return {
                                                            ...r,
                                                            children: beds
                                                        };
                                                    }
                                                })
                                        };
                                    })
                                };
                            })
                        };
                    });
                } else {
                    newBuildings = buildings.map(b => {
                        return {
                            ...b,
                            children: b.units.map(u => {
                                return {
                                    ...u,
                                    children: u.floors.map(f => {
                                        return {
                                            ...f,
                                            children: f.rooms
                                                .filter(r => r.enabled && r.roomStatus != 3)
                                                .map(r => {
                                                    return {
                                                        ...r,
                                                        children: r.beds.filter(b => b.enabled && !b.occupy)
                                                    };
                                                })
                                        };
                                    })
                                };
                            })
                        };
                    });
                }
                this.apartment.buildings = newBuildings;
            },
            async init() {
                this.contract = (await this.$axios.get(
                    api.contract + "/" + this.$props.id
                )).data;
                this.pricePackages = (await this.$axios.get(
                    "price-package/select-list"
                )).data;
                this.getBuilding(this.contract.apartmentId);
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
                            .post(api.contract + "/room-change", command)
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
