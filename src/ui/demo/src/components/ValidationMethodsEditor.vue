<template>
    <section>
        <el-button @click="showDialog = true" icon="el-icon-setting" circle></el-button>
        <span v-if="value">
            <template v-for="v in value">
                <el-tag style="margin-left:5px;" v-if="v.method === 'required'" :key="v.method" type="danger">必填</el-tag>
                <el-tag style="margin-left:5px;" v-if="isFormatValidation(v.method)" :key="v.method">{{getFormatValidationLabel(v.method)}}</el-tag>
            </template>
        </span>
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
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="showDialog = false">取 消</el-button>
                <el-button type="primary" @click="save">保 存</el-button>
            </div>
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
                isRequired: false,
                format: ""
            },
            showDialog: false,
            validations: [],
            validationMethods: []
        };
    },

    async mounted() {
        this.validations = JSON.parse(JSON.stringify(this.$props.value || []));
        this.setting.isRequired = this.validations.findIndex(x => x.method === "required") > -1;
        const formatValidations = this.validations.filter(x => ValidationMethods.isFormatValidation(x.method))
        this.setting.format = formatValidations.length > 0 ? formatValidations[0].method : ""
        this.validationMethods = ValidationMethods.items;
    },

    methods: {
        onIsRequiredChanged(val) {
            if (val) {
                this.validations.push({ method: "required" })
            } else {
                this.validations.splice(this.validations.findIndex(x => x.method === "required"), 1)
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
        addCustomValidation() { },
        isFormatValidation(method) {
            return ValidationMethods.isFormatValidation(method)
        },
        getFormatValidationLabel(method) {
            return ValidationMethods.items.filter(x => x.value === method)[0].label
        },
        save() {
            this.$emit("input", JSON.parse(JSON.stringify(this.validations)))
            this.showDialog = false;
        }
    }
};
</script>