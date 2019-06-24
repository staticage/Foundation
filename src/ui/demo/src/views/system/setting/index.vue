<template>
    <section>
        <el-form ref="form" :model="command" label-width="80px">
            <el-tabs v-model="tabActiveName">
                <el-tab-pane
                        v-for="(setting, settingIndex) in resource"
                        :label="setting.key"
                        :name="(settingIndex+1).toString()"
                        :key="settingIndex+1"
                >
                    <el-collapse v-model="activeNames">
                        <el-collapse-item
                                :title="obj.name"
                                :name="(index+1)"
                                v-for="(obj,index) in setting.children"
                                :key="index+1"
                        >
                            <el-row v-for="(prop,num) in obj.items" :key="num">
                                <el-col :span="8">
                                    <el-form-item label="属性名">
                                        <el-input v-model="prop.text" placeholder="请输入属性名"
                                                  :disabled="(obj.name == '客户级别' && num < 4)"></el-input>
                                    </el-form-item>
                                </el-col>
                                <el-col :span="8">
                                    <el-form-item label="属性值">
                                        <el-input-number
                                                v-model="prop.value"
                                                controls-position="right"
                                                :min="1"
                                                :max="100"
                                                :precision="0"
                                                label="请输入属性值"
                                                placeholder="请输入属性值"
                                                style="width:100%;"
                                                :disabled="(obj.name == '客户级别' && num < 4)">
                                        </el-input-number>
                                    </el-form-item>
                                </el-col>
                                <el-col :span="8" style="padding-left:12px;">
                                    <el-button @click="delButton(obj.items,num,obj.name)" v-if="(obj.name == '客户级别' && num > 3) && $hasPermission('删除')">
                                        删除
                                    </el-button>
                                    <el-button
                                            type="primary"
                                            plain
                                            v-if="(num+1)==obj.items.length"
                                            @click="addButton(obj.items,num+2,obj.name)"
                                    >添加
                                    </el-button>
                                </el-col>
                            </el-row>
                            <el-row>
                                <el-col :span="24" style="margin-bottom:18px;">
                                    <el-button type="primary" @click="save(obj.items,obj.name)">保存</el-button>
                                    <el-button>取消</el-button>
                                </el-col>
                            </el-row>
                        </el-collapse-item>
                    </el-collapse>
                </el-tab-pane>
            </el-tabs>
        </el-form>
    </section>
</template>
<script>
    import api from "../../../config/api";
    import _ from "lodash";

    export default {
        components: {},
        data() {
            return {
                loading: false,
                command: {},
                activeNames: [],
                tabActiveName: "1",
                resource: {}
            };
        },

        mounted() {
            this.search();
        },
        methods: {
            async search() {
                this.resource = (await this.$axios.get(api.setting)).data;
                // this.activeNames = Object.keys(this.resource).map((x, i) => i + 1);
            },

            delButton(element, index) {
                element.splice(index, 1);
                if (element.length == 0) {
                    this.addButton(element, 1);
                }
            },

            addButton(element, index) {
                element.push({
                    text: "text" + index,
                    value: index
                });
            },

            duplicates(arr) {
                var newArr = _.map(arr, "value").sort();
                var result = [];
                for (var i = 0; i < newArr.length; i++) {
                    if (newArr[i] == newArr[i + 1] && newArr[i] != newArr[i - 1]) {
                        result.push(newArr[i]);
                    }
                }
                return result;
            },

            save(value, name) {
                let valid = this.duplicates(value);
                if (valid.length === 0) {
                    this.loading = true;
                    this.$axios.post("setting/change", {
                        settingType: name,
                        items: value
                    }).then(
                        response => {
                            this.loading = false;
                            this.$message({
                                message: "保存成功",
                                type: "success"
                            });
                        }
                    ).catch(err => {
                        this.loading = false;
                        const result = response.response.data;
                        this.$message({
                            type: "error",
                            message: "保存失败:" + result.Message,
                            showClose: true,
                            duration: 0
                        });
                    });
                } else {
                    this.$message({
                        type: "error",
                        message: "保存失败:属性值【" + valid.slice(",") + "】重复"
                    });
                }
            }
        }
    };
</script>
<style>
</style>
