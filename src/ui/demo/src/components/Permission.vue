<template>
    <el-dialog :close-on-click-modal="false" v-loading="loading" title="权限设置" :visible.sync="dialogFormVisible" width="45%">
        <el-collapse v-model="activeNames">
            <el-collapse-item :title="data.name" :name="(index+1)" v-for="(data,index) in resource" :key="index">
                <div style="margin-bottom:10px;" v-for="children1 in data.children" :key="children1.value">
                    <el-row>
                        <el-col :span="24" style="padding-left:22px;">
                            <el-checkbox v-model="children1.select" @change="onSelectChange(children1.select,children1)">{{children1.name}}
                            </el-checkbox>
                        </el-col>
                    </el-row>
                    <el-row v-for="children2 in children1.children" :key="children2.value">
                        <el-col :span="6" :offset="2">
                            <el-checkbox
                                    v-model="children2.select"
                                    @change="onModuleSelectChange(children2.select,children2,children1)"
                            >{{children2.name}}
                            </el-checkbox>
                        </el-col>
                        <el-col :span="16">
                            <el-checkbox-group v-model="children2.selectedPermissions" @change="onCheckboxGroupSelectChange(children1,children2,children2.selectedPermissions)">
                                <el-checkbox
                                        v-for="(p,permIndex) in children2.accesses"
                                        :label="p"
                                        :key="(permIndex+1)"
                                >{{access[p]}}
                                </el-checkbox>
                            </el-checkbox-group>
                        </el-col>
                    </el-row>
                </div>
            </el-collapse-item>
        </el-collapse>
        <div slot="footer" class="dialog-footer">
            <el-button @click="dialogFormVisible = false">取 消</el-button>
            <el-button type="primary" @click="save('form')">确 定</el-button>
        </div>
    </el-dialog>
</template>

<script>
    import _ from "lodash";
    import axios from "../utils/http";
    import {accesses} from "../config/fullRoutes";

    export default {
        props: {
            id: {
                type: String,
                default: function () {
                    return "";
                }
            },
            data: {
                type: Array,
                default: function () {
                    return [];
                }
            },
            visible: {
                type: Boolean,
                default: function () {
                    return false;
                }
            }
        },

        data() {
            return {
                permissions: {},
                resource: [],
                resourceBackup: [],
                dialogFormVisible: false,
                loading: false,
                status: false, // 给父组件反馈一个刷新table状态
                activeNames: [],
                access: _.cloneDeep(accesses)
            };
        },

        mounted() {
            this.init();
            this.$watch("visible", newValue => {
                this.dialogFormVisible = newValue;
            });
            this.$watch("dialogFormVisible", newValue => {
                this.dialogFormVisible = newValue;
                if (newValue === false) {
                    this.$emit("callback", {
                        visible: this.dialogFormVisible,
                        status: this.status
                    });
                    this.reset();
                } else {
                    this.setSelected();
                }
            });
        },

        methods: {
            convert(bitmask) {
                var arr = [];
                for (let i = 0; i < 32; i++) {
                    let mask = 1 << i;
                    if ((bitmask & mask) != 0) {
                        arr.push(mask);
                    }
                }
                return arr;
            },
            init() {
                axios.get("permissions").then(response => {
                    response.data.forEach(first => {
                        if (first.children && first.children.length > 0) {
                            first.children.forEach(second => {
                                if (second.children && second.children.length > 0)
                                    second.children.forEach(third => {
                                        third.selectedPermissions = [];
                                        third.accesses = this.convert(third.permission);

                                    })
                            })
                        }

                    });
                    this.resource = response.data;
                    this.activeNames = Object.keys(this.resource).map((x, i) => i + 1);
                    this.resourceBackup = _.cloneDeep(this.resource);
                });
            },
            reset() {
                this.status = false;
                let newResource = _.cloneDeep(this.resourceBackup);
                newResource.forEach(data=>{
                    if(data.children && data.children.length > 0) {
                        data.children.forEach(x=>{
                            x.select = false;
                        })
                    }
                })
                this.resource = newResource;
            },
            setSelected() {
                if (this.$props.data.length > 0) {
                    let selected = this.$props.data;
                    let menus = _.cloneDeep(this.resource);
                    selected.forEach(s => {
                        let found = _.find(menus,x=>x.business == s.business);
                        s.children.forEach(c=>{
                            let secondFound = _.find(found.children,x=>x.category == c.category);
                            secondFound.select = true;
                            c.children.forEach(g=>{
                                let thirdFound = _.find(secondFound.children,x=>x.module == g.module);
                                thirdFound.select = true;
                                if(g.permission && g.permission > 0)
                                {
                                    let accesses = this.convert(g.permission);
                                    thirdFound.selectedPermissions = accesses;
                                }
                            })
                        })
                    })

                    this.resource = menus;
                }
            },
            getSource() {
                if (this.$props.data === undefined) {
                    return {};
                } else {
                    return JSON.parse(this.$props.data);
                }
            },

            sum(arr) {
                let result = 0;
                arr.forEach(x => {
                    result = result | x;
                })
                return result;
            },
            save() {
                let result = []
                this.resource.forEach(a => {
                    if (a.children && a.children.length > 0) {
                        a.children.forEach(b => {
                            if (b.children && b.children.length > 0) {
                                b.children.forEach(c => {
                                    if (c.selectedPermissions.length > 0) {
                                        result.push({
                                            business: a.business,
                                            category: b.category,
                                            module: c.module,
                                            permission: this.sum(c.selectedPermissions)
                                        })
                                    }
                                })
                            }


                        })
                    }
                })
                axios.post("role/permission", {
                        permissions: result,
                        id: this.$props.id
                    }).then(response => {
                            this.loading = false;
                            this.dialogFormVisible = false;
                            this.status = true;
                            this.$message({
                                type: "success",
                                message: "保存成功",
                                center: true
                            });
                        },
                        err => {
                            this.loading = false;
                            this.$message({
                                type: "error",
                                message: "保存失败:" + err.Message,
                                showClose: true,
                                duration: 0
                            });
                        }
                    );

            },
            onSelectChange(select, items) {
                items.children.forEach(feature => {
                    feature.select = select;
                    this.onModuleSelectChange(select, feature);
                });
            },
            onModuleSelectChange(select, feature, child1) {
                if (select) {
                    feature.selectedPermissions = this.convert(feature.permission);
                } else {
                    feature.selectedPermissions = new Array();
                }
                if (child1) {
                    this.$set(child1, 'select', select)
                }
                this.$emit("input", this.permissions);
            },
            onCheckboxGroupSelectChange(child1, child2, feature) {
                let selectState = feature.length > 0 ? true : false
                let child1SelectState = selectState
                child1.children.forEach(a => {
                    if (a.selectedPermissions.length > 0) {
                        child1SelectState = true
                        return
                    }
                })
                this.$set(child2, 'select', selectState)
                this.$set(child1, 'select', child1SelectState)
            }
        }
    };
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
    @import "../element-variables";

    .moreBtn {
        display: table;
        white-space: nowrap;
        border-spacing: 0px 0;
        font-size: 12px;
        color: $--color-primary;
        // margin-bottom: 18px;
    }

    .moreBtn:before,
    .moreBtn:after {
        display: table-cell;
        content: "";
        width: 50%;
        background: -webkit-linear-gradient(#eee, #eee) repeat-x left center;
        background: linear-gradient(#eee, #eee) repeat-x left center;
        background-size: 1px 1px;
    }

    .moreBtn sapn {
        cursor: pointer;
    }
</style>
