<template>
    <section>
        <el-form ref="form" :rules="rules" :model="command" label-width="120px" :disabled="disabled">
            <template v-for="(group, groupIndex) in customForm.fieldGroups">
                <el-row :key="`label${groupIndex}`" class="titie">{{group.label}}</el-row>
                <el-row :key="`input${groupIndex}`">
                    <el-col :span="8" v-for="field in group.fields" :key="field.name">
                        <el-form-item :label="field.label" :prop="field.name">
                            <el-input v-model="command[field.name]" :placeholder="`请输入${field.label}`"></el-input>
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
import validators, { ValidationMethods } from "../../../common/validators";
import ValidationMethodsEdtior from "../../../components/ValidationMethodsEditor";

// Vue.use(iviewArea);
export default {
    components: { ValidationMethodsEdtior },
    data() {
        return {
            rules: {},
            customForm: {},
            command: {},
            loading: true,
            companies: [],
            disabled: false,
            user: {},
            isEdit: false
        };
    },
    async mounted() {
        this.customForm = (await this.$axios.get(`custom-form/${this.$route.params.id}`)).data;
        this.rules = ValidationMethods.generateRules(this.customForm);
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
        back() {
            this.$router.go(-1);
        }
    }
};
</script>
<style>
</style>
