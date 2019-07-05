<template>
    <section>
        <el-form v-if="loaded" ref="form" :rules="rules" :model="command" label-width="120px" :disabled="disabled">
            <template v-for="(group, groupIndex) in customForm.fieldGroups">
                <el-row :key="`label${groupIndex}`" class="titie">{{group.label}}</el-row>
                <el-row :key="`input${groupIndex}`">
                    <el-col :span="12" v-for="field in group.fields" :key="field.name">
                        <el-form-item :label="field.label" :prop="field.name">
                            <el-input v-if="field.input.type === 'text'" v-model="command[field.name]" :placeholder="`请输入${field.label}`"></el-input>

                            <el-date-picker type="date" v-if="field.input.type === 'date'" v-model="command[field.name]" :placeholder="`请输入${field.label}`"></el-date-picker>

                            <el-select v-if="field.input.type === 'dropdown'" v-model="command[field.name]" :placeholder="`请输入${field.label}`">
                                <el-option v-for="(item, index) in field.input.options.items" :key="index" :label="item.label" :value="item.value"></el-option>
                            </el-select>

                            <el-radio-group v-if="field.input.type === 'radio'" v-model="command[field.name]" :placeholder="`请输入${field.label}`">
                                <el-radio v-for="(item, index) in field.input.options.items" :key="index" :label="item.value">{{item.label}}</el-radio>
                            </el-radio-group>
                            <el-checkbox-group v-if="field.input.type === 'checkbox'" v-model="command[field.name]">
                                <el-checkbox v-for="item in field.input.options.items" :label="item.value" :key="item.value">{{item.label}}</el-checkbox>
                            </el-checkbox-group>
                            <el-input-number v-if="field.input.type === 'number'" v-model="command[field.name]"></el-input-number>
                        </el-form-item>
                    </el-col>
                </el-row>
            </template>
        </el-form>
        <el-row style="margin-top:18px;">
            <el-button type="primary" @click="save" v-if="!disabled">保存</el-button>
            <el-button @click="back">取消</el-button>
        </el-row>
    </section>
</template>
<script>
import ElementUI from "element-ui";
import Vue from "vue";
import validators, { ValidationMethods } from "../../../common/validators";
import ValidationMethodsEdtior from "../../../components/ValidationMethodsEditor";
const cityOptions = ['上海', '北京', '广州', '深圳'];
// Vue.use(iviewArea);
export default {
    components: { ValidationMethodsEdtior },
    data() {
        return {

            loaded: false,
            rules: {},
            customForm: {},
            command: {

            },
            loading: true,
            companies: [],
            disabled: false,
            user: {
                test: []
            },
            isEdit: false
        };
    },
    async mounted() {
        let customForm = (await this.$axios.get(`custom-form/${this.$route.params.id}`)).data
        this.rules = ValidationMethods.generateRules(customForm)
        this.fields = customForm.fieldGroups
            .map(x => x.fields)
            .reduce((x, y) => x.concat(y), [])
        await this.loadOptions(customForm)
        this.generateFieldDefaultValues()
        this.customForm = customForm
        this.loaded = true
    },
    methods: {
        async save() {
            const valid = await this.$refs.form.validate();
            if (valid) {
                this.loading = true;
                console.log("command", this.command);
                await this.$axios.post(`entity/${this.customForm.name}`, this.command);
                this.loading = false;
            }
        },
        generateFieldDefaultValues() {
            this.fields.forEach(field => {
                let defaultValue = null;
                if (field.input.options && field.input.options.items && field.input.options.items.length) {
                    if (field.input.type === "dropdown" || field.input.type === "radio") {
                        defaultValue = field.input.options.items[0].value;
                    }
                }
                Vue.set(this.command, field.name, defaultValue || ValidationMethods.inputs[field.input.type].defaultValue())
            })
        },
        back() {
            this.$router.go(-1);
        },

        async loadOptions(customForm) {
            const loadOptionsTasks = this.fields
                .filter(x => x.input.options)
                .map(x => this.loadOptionsData(x.input.options))

            await Promise.all(loadOptionsTasks);
        },

        async loadOptionsData(optionsSetting) {
            if (optionsSetting.type === 1) {
                optionsSetting.items = (await this.$axios.get(`setting/${optionsSetting.settingId}`)).data.items.filter(x => x.enabled)
            } else if (optionsSetting.type === 2) {

            }
        }
    }
};
</script>
<style>
</style>
