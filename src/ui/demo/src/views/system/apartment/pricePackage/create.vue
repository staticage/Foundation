<template>
    <section>
        <el-form ref="form" :model="command" :rules="rules" label-width="100px" :disabled="disabled">
            <el-row class="titie">基础信息</el-row>
            <el-row>
                <el-col :span="8">
                    <el-form-item label="公寓名称" prop="apartmentId">
                        <el-select v-model="command.apartmentId" placeholder="请选择公寓名称" style="width:100%;" @change="apartmentChange">
                            <el-option label="全部" value></el-option>
                            <el-option v-for="item in apartments" :key="item.id" :label="item.name" :value="item.id"></el-option>
                        </el-select>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="费用包名称" prop="name">
                        <el-input v-model="command.name" placeholder="请输入费用包名称"></el-input>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="开始有效期" prop="startTime">
                        <el-col :span="11">
                            <el-date-picker v-model="command.startTime" type="date" placeholder="选择日期时间" style="width:100%;"></el-date-picker>
                        </el-col>
                        <el-col :span="2" style="text-align: center;">-</el-col>
                        <el-col :span="11">
                            <el-date-picker v-model="command.endTime" type="date" placeholder="选择日期时间" style="width:100%;"></el-date-picker>
                        </el-col>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row class="titie">费用包</el-row>
            <el-row v-for="(prop,index) in itemType.roomtypes" :key="'A-'+index">
                <!-- :class="prop.separator == true ? 'roomtype-separator' : ''" -->
                <el-col :span="8">
                    <el-form-item label="房型名称">
                        <el-input disabled v-model="prop.name" placeholder="请输入房型名称"></el-input>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="价格">
                        <!-- :prop="'price' + index"
                        :rules="{required: true, type:'number', message: '请输入价格', trigger: 'blur'}"-->
                        <el-input-number v-model="prop.price" controls-position="right" :min="0" :precision="2" label="请输入价格" placeholder="请输入价格" style="width:100%;"></el-input-number>
                    </el-form-item>
                </el-col>
                <el-col :span="8" style="padding-left:12px;">
                    <el-button @click="delButton(index,0)" v-if="$hasPermission('删除')">删除</el-button>
                    <el-button type="primary" plain v-if="(index+1)==itemType.roomtypes.length" @click="addButton(index+2,0)">添加</el-button>
                </el-col>
            </el-row>
            <el-row v-for="(prop,index) in itemType.servicePack" :key="'B-'+index">
                <el-col :span="8">
                    <el-form-item label="名称">
                        <el-input v-model="prop.name" placeholder="请输入名称"></el-input>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="价格">
                        <el-input-number v-model="prop.price" controls-position="right" :min="0" :precision="2" label="请输入价格" placeholder="请输入价格" style="width:100%;"></el-input-number>
                    </el-form-item>
                </el-col>
                <el-col :span="8" style="padding-left:12px;">
                    <el-button @click="delButton(index,1)" v-if="$hasPermission('删除')">删除</el-button>
                    <el-button type="primary" plain v-if="(index+1)==itemType.servicePack.length" @click="addButton(index+2,1)">添加</el-button>
                </el-col>
            </el-row>
            <el-row v-for="(prop,index) in itemType.others" :key="'C-'+index">
                <el-col :span="8">
                    <el-form-item label="生活能力名称">
                        <el-input v-model="prop.name" placeholder="请输入生活能力名称"></el-input>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="价格">
                        <el-input-number v-model="prop.price" controls-position="right" :min="0" :precision="2" label="请输入价格" placeholder="请输入价格" style="width:100%;"></el-input-number>
                    </el-form-item>
                </el-col>
                <el-col :span="8" style="padding-left:12px;">
                    <el-button @click="delButton(index,2)" v-if="$hasPermission('删除')">删除</el-button>
                    <el-button type="primary" plain v-if="(index+1)==itemType.others.length" @click="addButton(index+2,2)">添加</el-button>
                </el-col>
            </el-row>
            <el-row v-for="(prop,index) in itemType.deposits" :key="'D-'+index">
                <el-col :span="8">
                    <el-form-item label="押金类型名称">
                        <el-input v-model="prop.name" placeholder="请输入押金类型名称"></el-input>
                    </el-form-item>
                </el-col>
                <el-col :span="8">
                    <el-form-item label="价格">
                        <el-input-number v-model="prop.price" controls-position="right" :min="0" :precision="2" label="请输入价格" placeholder="请输入价格" style="width:100%;"></el-input-number>
                    </el-form-item>
                </el-col>
                <el-col :span="8" style="padding-left:12px;">
                    <el-button @click="delButton(index,3)" v-if="$hasPermission('删除')">删除</el-button>
                    <el-button type="primary" plain v-if="(index+1)==itemType.deposits.length" @click="addButton(index+2,3)">添加</el-button>
                </el-col>
            </el-row>
        </el-form>
        <el-row style="margin-top:18px;">
            <el-button type="primary" @click="save()" v-if="!disabled">保存</el-button>
            <el-button @click="back">取消</el-button>
        </el-row>
    </section>
</template>
<script>
import ElementUI from "element-ui";
import AreaSelector from "@/components/AreaSelector.vue";
import api from "@/config/api";
import Configurations from "@/common/Configurations";
import _ from "lodash";
import moment from "moment";

// Vue.use(iviewArea);
export default {
    components: { AreaSelector },
    data() {
        return {
            disabled: false,
            command: {
                priceItems: []
            },
            initServicePack: [{ name: "服务费", type: 1 }, { name: "餐费", type: 1 }],
            initOthers: [],
            initDeposits: [],
            itemType: {
                // enum Type
                // RoomType,
                // ServicePack,
                // Other
                roomtypes: [],
                servicePack: [],
                others: [],
                deposits: []
            },
            loading: false,
            apartments: [],
            rules: {
                name: [{ required: true, message: "请输入费用名称", trigger: "blur" }],
                apartmentId: [
                    { required: true, message: "请选择公寓名称", trigger: "change" }
                ],
                startTime: [
                    {
                        type: "date",
                        required: true,
                        message: "请选择开始有效期",
                        trigger: "change"
                    }
                ]
                // consultantRelation: [{ validator: this.validateConsultantRelation, trigger: "change" }]
            }
        };
    },

    // Component引入后才可执行该方法
    async mounted() {
        // 房型 服务包 其他 押金
        this.initOthers = await Configurations.getConfiguration(
            "合同项配置",
            "生活能力"
        );
        this.initOthers.forEach(element => {
            element.name = element.text;
            element.type = 2;
        });
        this.initDeposits = await Configurations.getConfiguration(
            "合同项配置",
            "押金类型"
        );
        this.initDeposits.forEach(element => {
            element.name = element.text;
            element.type = 3;
        });
        console.log(this.$route.params);
        if (this.$route.params.disabled == true) {
            this.disabled = true;
        }
        if (this.initOthers.length > 0 && this.initDeposits.length > 0) {
            this.apartments = (await this.$axios.get("apartment/select-list")).data;
            if (this.apartments.length > 0) {
                if (this.$route.params.id !== undefined) {
                    this.command = (await this.$axios.get(
                        "apartment/price-package/" + this.$route.params.id
                    )).data;
                    this.itemType.roomtypes = this.getItemType(
                        this.command.priceItems,
                        0
                    );
                    this.itemType.servicePack = this.getItemType(
                        this.command.priceItems,
                        1
                    );
                    this.itemType.others = this.getItemType(this.command.priceItems, 2);
                    this.itemType.deposits = this.getItemType(this.command.priceItems, 3);

                    if (this.command.startTime)
                        this.command.startTime = moment(this.command.startTime).toDate();
                    if (this.command.endTime)
                        this.command.endTime = moment(this.command.endTime).toDate();

                    if (this.$route.params.clone == true) {
                        this.command.id = undefined;
                        this.command.name = undefined;
                        this.command.startTime = undefined;
                        this.command.endTime = undefined;
                    }
                }
                this.$refs["form"].clearValidate();
            } else {
                this.addApartment();
            }
        } else {
            this.addSetting();
        }
    },
    methods: {
        async apartmentChange(apartmentId) {
            if (apartmentId) {
                this.itemType.roomtypes = (await this.$axios.get(
                    "roomtype/select-list/" + apartmentId
                )).data;
                if (this.itemType.roomtypes.length <= 0) {
                    this.addRoomType();
                } else {
                    this.itemType.roomtypes.forEach(element => {
                        element.type = 0;
                    });
                    this.itemType.servicePack = _.clone(this.initServicePack);
                    this.itemType.others = _.clone(this.initOthers);
                    this.itemType.deposits = _.clone(this.initDeposits);
                }
            } else {
                this.itemType.roomtypes = [];
                this.itemType.servicePack = [];
                this.itemType.others = [];
                this.itemType.deposits = [];
            }
        },

        getItemType(priceItems, type) {
            let data = priceItems.filter(x => x.type == type);
            if (data) {
                return data;
            }
            return [];
        },

        // 未添加房型 房型提示添加
        addRoomType() {
            this.$confirm("该公寓没有房型无法添加费用包,是否去添加房型", "提示", {
                confirmButtonText: "添加",
                cancelButtonText: "取消",
                type: "warning"
            })
                .then(() => {
                    this.$router.push({
                        name: "room-type"
                    });
                })
                .catch(() => {
                    // this.$router.go(-1);
                });
        },

        // 未添加公寓 提示添加
        addApartment() {
            this.$confirm("没有公寓无法添加费用包,是否去添加公寓", "提示", {
                confirmButtonText: "添加",
                cancelButtonText: "取消",
                type: "warning"
            })
                .then(() => {
                    this.$router.push({
                        path: "/system/apartment/create/undefined/false"
                    });
                })
                .catch(() => {
                    this.$router.go(-1);
                });
        },

        // 未添加设置配置项 提示添加
        addSetting() {
            let text = [];
            if (this.initOthers.length > 0) {
                text.push("生活能力");
            }
            if (this.initDeposits.length > 0) {
                text.push("押金类型");
            }
            this.$confirm("没有[" + text.join(",") + "]选项,是否去添加", "提示", {
                confirmButtonText: "添加",
                cancelButtonText: "取消",
                type: "warning"
            })
                .then(() => {
                    this.$router.push({
                        name: "system-setting"
                    });
                })
                .catch(() => {
                    this.$router.go(-1);
                });
        },

        delButton(index, type) {
            switch (type) {
                case 0:
                    this.itemType.roomtypes.splice(index, 1);
                    if (this.itemType.roomtypes.length === 0) {
                        this.addButton(1, type);
                    }
                    break;
                case 1:
                    this.itemType.servicePack.splice(index, 1);
                    if (this.itemType.servicePack.length === 0) {
                        this.addButton(1, type);
                    }
                    break;
                case 2:
                    this.itemType.others.splice(index, 1);
                    if (this.itemType.others.length === 0) {
                        this.addButton(1, type);
                    }
                    break;
                case 3:
                    this.itemType.deposits.splice(index, 1);
                    if (this.itemType.deposits.length === 0) {
                        this.addButton(1, type);
                    }
                    break;
            }
        },

        addButton(index, type) {
            switch (type) {
                case 0:
                    this.itemType.roomtypes.push({
                        name: "name" + index,
                        price: 0,
                        type: type
                    });
                    break;
                case 1:
                    this.itemType.servicePack.push({
                        name: "name" + index,
                        price: 0,
                        type: type
                    });
                    break;
                case 2:
                    this.itemType.others.push({
                        name: "name" + index,
                        price: 0,
                        type: type
                    });
                    break;
                case 3:
                    this.itemType.deposits.push({
                        name: "name" + index,
                        price: 0,
                        type: type
                    });
                    break;
            }
        },

        duplicates() {
            var newArr = _.map(this.command.priceItems, "name").sort();
            var result = [];
            for (var i = 0; i < newArr.length; i++) {
                if (newArr[i] == newArr[i + 1] && newArr[i] != newArr[i - 1]) {
                    result.push(newArr[i]);
                }
            }
            return result;
        },
        setPriceItems() {
            this.command.priceItems = this.itemType.roomtypes
                .concat(this.itemType.servicePack)
                .concat(this.itemType.others)
                .concat(this.itemType.deposits);
        },
        save() {
            this.setPriceItems();
            if (this.command.priceItems.length === 0) {
                this.$message({
                    type: "error",
                    message: "保存失败:费用包围为空"
                });
                return false;
            }
            this.$refs.form.validate(valid => {
                if (valid) {
                    let validDuplicate = this.duplicates();
                    if (validDuplicate.length === 0) {
                        this.loading = true;
                        const resopne = this.command.id
                            ? this.$axios.put("price-package", this.command)
                            : this.$axios.post("apartment/price-package", this.command);
                        resopne.then(
                            response => {
                                this.loading = false;
                                this.$message({
                                    message: "保存成功",
                                    type: "success"
                                });
                                this.back();
                            },
                            err => {
                                this.loading = false;
                                const result = err.response.data;
                                this.$message({
                                    type: "error",
                                    message: "保存失败:" + result.Message
                                });
                            }
                        );
                    } else {
                        this.$message({
                            type: "error",
                            message: "保存失败:名称【" + validDuplicate.slice(",") + "】重复"
                        });
                    }
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
.roomtype-separator {
    border-top: 0.5px dashed #e6e6e6;
    padding-top: 18px;
}
</style>
