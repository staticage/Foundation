<template>
    <section>
        <el-button @click="showDialog = true">设置</el-button>
        <el-tag></el-tag>
        <el-dialog title="验证设置" :visible.sync="showDialog" width="40%">
            <el-form ref="form" :model="setting" label-width="80px" label-position="left">
                <el-form-item label="是否必填">
                    <el-switch v-model="setting.isRequired" @change="onIsRequiredChanged"></el-switch>
                </el-form-item>
                <el-form-item label="格式验证">
                    <el-radio-group v-model="setting.format" @change="onFormatValidationChanged">
                        <el-radio label>无</el-radio>
                        <el-radio :key="index" v-for="(method, index) in validationMethods" :label="method.value">{{method.label}}</el-radio>
                    </el-radio-group>
                </el-form-item>

                <el-button @click="addCustomValidation">添加自定义验证</el-button>
            </el-form>
        </el-dialog>
    </section>
</template>

<script>
import { ValidationMethods } from "../common/validators"
export default {
    props: {
        value: {
            type: Array,
            default: function () {
                return [];
            }
        }
    },
    data() {
        return {
            isRequired: false,
            setting: {
                custom: [],
                format: ""
            },
            showDialog: false,
            validations: [],
            validationMethods: []
        };
    },

    async mounted() {
        this.validations = this.$props.value || [];
        this.setting.isRequired = this.validations.findIndex(x => x.method === "required") > -1;
        const formatValidations = this.validations.filter(x => ValidationMethods.isFormatValidation(x.method))
        this.setting.format = formatValidations.length > 0 ? formatValidations[0].method : ""
        this.validationMethods = ValidationMethods.items;
    },

    methods: {
        onIsRequiredChanged(val) {
            if (val) {
                this.$props.value.push({ method: "required" })
            } else {
                this.$props.value.splice(this.$props.value.findIndex(x => x.method === "required"), 1)
            }
        },
        onFormatValidationChanged(e) {
            ValidationMethods.items.forEach(item => {
                const index = this.validations.findIndex(x => x.method === item.value)
                if (index > -1) {
                    this.validations.splice(index, 1)
                }
            });

            if (e) {
                this.validations.push({ method: e })
            }
        },
        async showConsumables(row) {
            this.showDialog = true;
            this.consumables = (await this.$axios.get(
                api.valueAddedService + "/consumable/" + row.id
            )).data
        },
        addCustomValidation() { },
        async search() {
            this.resource = (await this.$axios.post(
                api.valueAddedService + "/query",
                this.query
            )).data;
        },
        init() {
            this.query.customerId = this.$props.id;
            this.search();
        },
        edit() {
            this.dialogFormVisible = true;
        },
    }
};
</script>