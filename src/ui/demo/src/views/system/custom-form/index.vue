<template>
    <section>
        <el-row class="button-top-row">
            <el-col :span="12">
                <el-select v-model="metadataName" placeholder="请选择" @change="onMetadataNameChanged">
                    <el-option v-for="item in metadata" :key="item.type" :label="item.label" :value="item.typeMetadata.name"></el-option>
                </el-select>
            </el-col>
            <el-col :span="12">
                <el-button type="primary" :disabled="!customForm || !customForm.id" @click="preview">预览</el-button>
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="24">
                <el-button type="primary" @click="save">保存</el-button>
            </el-col>
        </el-row>
        <el-row class="table-row" v-for="(item, index) in this.customForm.fieldGroups" :key="index">
            <el-input v-model="item.label" placeholder></el-input>
            <el-table :data="item.fields">
                <el-table-column prop="name" label="字段" width="120"></el-table-column>
                <el-table-column prop="label" label="显示名称" width="160">
                    <template slot-scope="scope">
                        <el-input v-model="scope.row.label" placeholder></el-input>
                    </template>
                </el-table-column>
                <el-table-column label="字段类型" width="120">
                    <template slot-scope="scope">
                        <el-select v-model="scope.row.input.type" placeholder="请选择">
                            <el-option v-for="item in fieldTypes" :key="item.value" :label="item.label" :value="item.value"></el-option>
                        </el-select>
                    </template>
                </el-table-column>
                <el-table-column prop="phone" label="验证规则">
                    <template slot-scope="scope">
                        <ValidationMethodsEdtior v-model="scope.row.input.validationMethods" />
                    </template>
                </el-table-column>
                <el-table-column prop="phone" label="字段选项">
                    <template slot-scope="scope">
                        <OptionsEditor v-model="scope.row.input.options" :settings="settings" />
                    </template>
                </el-table-column>

                <el-table-column align="center" width="70" prop="phone" label="列表显示">
                    <template slot-scope="scope">
                        <el-switch v-model="scope.row.isShowInTable"></el-switch>
                    </template>
                </el-table-column>
            </el-table>
        </el-row>

        <el-dialog :close-on-click-modal="false" title="机构信息" :visible.sync="dialogFormVisible" width="30%">
            <el-form ref="dialogForm" :rules="rules" :model="dialogForm" label-width="80px">
                <el-form-item label="机构名称" prop="name">
                    <el-input v-model="dialogForm.name" placeholder="请输入机构名称"></el-input>
                </el-form-item>
                <el-form-item label="机构法人" prop="corporation">
                    <el-input v-model="dialogForm.corporation" placeholder="请输入机构法人"></el-input>
                </el-form-item>
                <el-form-item label="联系电话" prop="phone">
                    <el-input v-model="dialogForm.phone" placeholder="请输入联系电话"></el-input>
                </el-form-item>
                <el-form-item label="电子邮箱" prop="email">
                    <el-input v-model="dialogForm.email" placeholder="请输入电子邮箱"></el-input>
                </el-form-item>
                <el-form-item label="账户名称" prop="accountName">
                    <el-input v-model="dialogForm.accountName" placeholder="请输入账户名称"></el-input>
                </el-form-item>
                <el-form-item label="账户号码" prop="accountNumber">
                    <el-input v-model="dialogForm.accountNumber" placeholder="请输入账户号码"></el-input>
                </el-form-item>
                <el-form-item label="开户行" prop="bankName">
                    <el-input v-model="dialogForm.bankName" placeholder="请输入开户行"></el-input>
                </el-form-item>
                <el-form-item label="邮政编码" prop="postcode">
                    <el-input v-model="dialogForm.postcode" placeholder="请输入邮政编码"></el-input>
                </el-form-item>
                <el-form-item label="机构地址" prop="address">
                    <el-input v-model="dialogForm.address" placeholder="请输入机构地址"></el-input>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="cancel">取 消</el-button>
                <el-button type="primary" @click="save">确 定</el-button>
            </div>
        </el-dialog>
    </section>
</template>
<script >
import api from "../../../config/api";
import _ from "lodash";
import validators, { ValidationMethods } from "../../../common/validators";
import ValidationMethodsEdtior from "../../../components/ValidationMethodsEditor";
import OptionsEditor from "../../../components/OptionsEditor";
export default {
    components: { ValidationMethodsEdtior, OptionsEditor },
    data() {
        return {
            type: '',
            dialogForm: { id: "" },
            query: {},
            resource: {},
            dialogFormVisible: false,
            loading: false,
            model: {},
            customFields: [
                {
                    label: "基础信息",
                    fields: [
                        {
                            type: "Text",
                            label: "姓名"
                        }
                    ]
                }
            ],
            metadata: [],
            metadataName: '',
            user: {},
            fieldTypes: [],
            customForm: {
                name: ''
            },
            rules: {},
            settings: []
        };
    },

    async mounted() {
        this.user = this.$store.state.user;
        this.fieldTypes = ValidationMethods.getInputs();
        this.metadata = (await this.$axios.get("custom-form/_metadata")).data;
        this.settings = (await this.$axios.post('setting/_query', {})).data
    },
    methods: {
        async onMetadataNameChanged(e) {
            const meta = this.metadata.filter(x => x.typeMetadata.name === this.metadataName)[0];
            const fields = meta.properties.map(x => ({ name: x.name, label: x.label, isShowInTable: true, input: { type: 'text', validatonMethods: [] } }));
            const existCustomForms = (await this.$axios.post("custom-form/_query", { name: this.metadataName })).data

            if (existCustomForms && existCustomForms.length) {
                const mergedFields = _.unionBy(existCustomForms[0].fieldGroups[0].fields, fields, 'name');
                existCustomForms[0].fieldGroups[0].fields = mergedFields;
                this.customForm = existCustomForms[0]
            } else {
                this.customForm = {
                    label: meta.label,
                    name: meta.typeMetadata.name,
                    fieldGroups: [
                        {
                            label: '基础信息',
                            fields: fields
                        }
                    ]
                }
            }
        },
        async save() {
            if (!this.customForm.id) {
                await this.$axios.post("custom-form", this.customForm);
            }
            else {
                await this.$axios.put("custom-form", this.customForm);
            }
        },
        async preview() {
            this.$router.push(`/system/custom-form/${this.customForm.id}/preview`);
        },
        async search() {
            this.resource = (await this.$axios.post(
                api.company + "/query",
                this.query
            )).data;
        },
        create() {
            this.dialogForm = { id: "" };
            this.dialogFormVisible = true;
            if (this.$refs["dialogForm"]) {
                this.$refs["dialogForm"].clearValidate();
            }
        },
        async editShow(id) {
            this.dialogForm = (await this.$axios.get(
                api.company + "/" + id
            )).data;
            this.dialogFormVisible = true;
            this.$refs["dialogForm"].clearValidate();
        },

        cancel() {
            this.dialogFormVisible = false;
            this.$refs["dialogForm"].clearValidate(); // 重置验证
        }
    }
};
</script>
