<template>
    <section>
        <el-button @click="showDialog = true" icon="el-icon-setting" circle></el-button>

        <el-tag style="margin-left:5px;" v-if="value.type === 1">{{settingName}}</el-tag>
        <el-tooltip v-if="value.type === 2" :content="value.apiUrl">
            <el-tag style="margin-left:5px;" type="info">Web Api</el-tag>
        </el-tooltip>

        <el-dialog title="选项设置" :visible.sync="showDialog" width="40%">
            <el-form ref="form" :model="options" label-width="80px" label-position="left">
                <el-form-item label="数据来源">
                    <el-radio-group v-model="options.type" @change="onTypeChanged">
                        <el-radio :label="1">数据字典</el-radio>
                        <el-radio :label="2">Web Api</el-radio>
                    </el-radio-group>
                </el-form-item>
                <section v-if="options.type === 1">
                    <el-form-item label="数据字典" prop="settingId" :rules="{required: true, message: '数据字典不能为空', trigger: 'change'}">
                        <el-select placeholder="请选择数据字典" v-model="options.settingId">
                            <el-option v-for="item in settings" :key="item.id" :label="item.name" :value="item.id"></el-option>
                        </el-select>
                    </el-form-item>
                </section>
                <section v-if="options.type === 2">
                    <el-form-item label="Api地址" prop="apiUrl" :rules="{required: true, message: 'Api地址不能为空', trigger: 'blur'}">
                        <el-input v-model="options.apiUrl" placeholder></el-input>
                    </el-form-item>
                </section>
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
            type: Object,
            default: function () {
                return {};
            }
        },
        settings: {
            type: Array,
            default: function () {
                return {};
            }
        }
    },
    data() {
        return {
            isRequired: false,
            options: {
                type: 1
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
        this.options = JSON.parse(JSON.stringify(this.$props.value || { type: 1 }))
    },
    computed: {
        settingName() {
            const filtered = this.settings.filter(x => x.id === this.value.settingId);
            return filtered.length > 0 ? filtered[0].name : "";
        }
    },
    methods: {
        onTypeChanged() {
            this.$refs.form && this.$refs.form.clearValidate();
        },
        init() {
            this.query.customerId = this.$props.id;
            this.search();
        },
        edit() {
            this.dialogFormVisible = true;
        },
        getSettingName() {
            return this.settings.filter(x => x.id === this.$props.value.settingId).name;
        },
        async save() {
            const valid = await this.$refs.form.validate();
            if (valid) {
                this.$emit("input", JSON.parse(JSON.stringify(this.options)))
                this.showDialog = false;
            }
        }
    }
};
</script>