<template>
    <section>
        <el-button @click="showDialog = true" icon="el-icon-setting" circle></el-button>

        <el-tag style="margin-left:5px;" v-if="value.type && value.type !== 0">{{settingName}}</el-tag>

        <el-dialog title="查询设置" :visible.sync="showDialog" width="40%">
            <el-form ref="form" :model="options" label-width="80px" label-position="left">
                <el-form-item label="查询方式">
                    <el-radio-group v-model="options.type">
                        <el-radio :label="0">无</el-radio>
                        <el-radio :label="1">精确查询</el-radio>
                        <el-radio :label="2">模糊查询</el-radio>
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
            type: Object,
            default: function () {
                return { type: 0 };
            }
        }
    },
    data() {
        return {
            isRequired: false,
            options: {
                type: 0
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
        this.options = JSON.parse(JSON.stringify(this.$props.value))
    },
    computed: {
        settingName() {
            const filtered = ValidationMethods.queryOptions.filter(x => x.value === this.value.type);
            return filtered.length > 0 ? filtered[0].label : "";
        }
    },
    methods: {
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