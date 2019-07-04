<template>
    <section>
        <el-button @click="showDialog = true" icon="el-icon-setting" circle></el-button>

        <el-tag style="margin-left:5px;" v-if="value.type === '1'"></el-tag>

        <el-dialog title="选项设置" :visible.sync="showDialog" width="40%">
            <el-form ref="form" :model="options" label-width="80px" label-position="left">
                <el-form-item label="数据来源">
                    <el-radio-group v-model="options.type">
                        <el-radio label="1">数据字典</el-radio>
                        <el-radio label="2">Api</el-radio>
                    </el-radio-group>
                </el-form-item>
                <section v-if="options.type === '1'">
                    <el-form-item label="数据字典">
                        <el-select placeholder="请选择数据字典" v-model="options.settingId">
                            <el-option v-for="item in settings" :key="item.id" :label="item.name" :value="item.id"></el-option>
                        </el-select>
                    </el-form-item>
                </section>
                <section v-if="options.type === '2'">
                    <el-form-item label="Api地址">
                        <el-input v-model="options.apiUrl" placeholder></el-input>
                    </el-form-item>
                </section>
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
            settings: [],
            options: {
                type: "1"
            },
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
        this.options = JSON.parse(JSON.stringify(this.$props.value || { type: "1" }))
        this.settings = (await this.$axios.get("setting/_select-list")).data;
        this.setting.isRequired = this.validations.findIndex(x => x.method === "required") > -1;
        const formatValidations = this.validations.filter(x => ValidationMethods.isFormatValidation(x.method))
        this.setting.format = formatValidations.length > 0 ? formatValidations[0].method : ""
        this.validationMethods = ValidationMethods.items;
    },

    methods: {

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