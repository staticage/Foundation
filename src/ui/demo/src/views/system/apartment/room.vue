<template>
    <section>
        <el-tree :data="treeData" node-key="id" v-loading="loading" default-expand-all empty-text="正在加载">
            <!-- <el-tree :data="treeData" :expand-on-click-node="false"> -->
            <span class="custom-tree-node" slot-scope="{ node, data }">
                <span>&nbsp;&nbsp;{{node.label }} {{data.roomTypeName?'('+data.roomTypeName+')':''}}</span>
                <span>
                    <el-button icon="el-icon-plus" size="mini" v-if="node.level!==config.length" @click.stop="append(node,data)" circle></el-button>
                    <el-button icon="el-icon-edit" size="mini" @click.stop="edit(node,data)" circle></el-button>
                    <el-button icon="el-icon-delete" size="mini" @click.stop="remove(node, data)" circle></el-button>
                </span>
            </span>
        </el-tree>
        <el-dialog :close-on-click-modal="false" v-loading="loading" :title="text+'信息'" :visible.sync="dialogFormVisible" width="30%">
            <el-form ref="form" :rules="rules" :model="command" label-width="80px">
                <el-form-item :label="text+'房型'" v-if="command.level==4" prop="roomtypeId">
                    <el-select v-model="command.roomTypeId" placeholder="请选择房型" style="width:100%;">
                        <el-option v-for="item in roomtypes" :key="item.id" :label="item.name" :value="item.id"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item :label="text" prop="name">
                    <el-input :placeholder="'请输入'+text" v-model="command.name"></el-input>
                </el-form-item>
                <el-form-item label="是否启用" v-if="(command.level==4||command.level==5)">
                    <el-switch v-model="command.enabled"></el-switch>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="back">取 消</el-button>
                <el-button type="primary" @click="save('form')">确 定</el-button>
            </div>
        </el-dialog>
    </section>
</template>
<script >
    import AreaSelector from "../../../components/AreaSelector.vue";
    import api from "../../../config/api";
    import * as types from "../../../store/types";
    import axios from "../../../utils/http";
    import _ from "lodash";

    export default {
        components: { AreaSelector },
        data() {
            return {
                roomtypes: [],
                config: [
                    { name: "楼栋", level: 1, path: "building" },
                    { name: "单元", level: 2, path: "unit" },
                    { name: "楼层", level: 3, path: "floor" },
                    { name: "房间", level: 4, path: "room" },
                    { name: "床位", level: 5, path: "bed" }
                ],
                text: "",
                disabledType: false,
                activeNames: [],
                command: {},
                loading: false,
                selectedOptions: [],
                user: {},
                treeData: [],
                rules: {},
                dialogFormVisible: false,
                resource: {},
                apartmentId: ""
            };
        },

        async mounted() {
            if (this.$route.params.id) {
                this.apartmentId = this.$route.params.id;
                this.roomtypes = (await axios.get(
                    "roomtype/select-list/" + this.apartmentId
                )).data;
                this.search();
            }

            this.user = this.$store.state.user;
            if (this.user.supperAdmin === false) {
                this.command.companyId = this.user.companyId;
            }
        },
        methods: {
            async search() {
                this.loading = true;
                this.dialogFormVisible = false;
                await this.$axios
                    .get("apartment/building/" + this.apartmentId)
                    .then(
                        response => {
                            this.loading = false;
                            this.resource = response.data;
                            this.treeData = this.resource.buildings;
                        },
                        err => {
                            this.loading = false;
                            this.$message({
                                type: "error",
                                message: "删除失败:" + err.message,
                                center: true
                            });
                        }
                    );
            },
            append(node, data) {
                let parentId = data.id;
                let addNode = _.clone(node);
                addNode.level++;
                this.showEdit(addNode);
                this.command = {
                    level: addNode.level,
                    enabled: true,
                    apartmentId: this.apartmentId,
                    buildingId: parentId,
                    unitId: parentId,
                    floorId: parentId,
                    roomId: parentId
                };
            },

            edit(node, data) {
                this.showEdit(node);
                this.command = {
                    id: data.id,
                    name: data.name,
                    level: node.level,
                    enabled: data.enabled,
                    roomTypeId: data.roomTypeId
                };
            },

            showEdit(node) {
                this.text = this.config[node.level - 1].name;
                this.dialogFormVisible = true;
            },

            remove(node, data) {
                this.$confirm("确认删除?", "提示", {
                    confirmButtonText: "确定",
                    cancelButtonText: "取消",
                    type: "warning"
                })
                    .then(() => {
                        this.$axios
                            .delete(this.getPath(node.level) + "/" + data.id)
                            .then(
                                response => {
                                    this.loading = false;
                                    this.$message({
                                        type: "success",
                                        message: "删除成功!"
                                    });
                                    this.search();
                                },
                                response => {
                                    this.loading = false;
                                    const result = response.response.data;
                                    this.$message({
                                        type: "error",
                                        message: "删除失败:" + result.error,
                                        center: true
                                    });
                                }
                            );
                    })
                    .catch(() => {
                        // catch 不要删除，Uncaught (in promise) cancel
                    });
            },
            getPath(level) {
                let path = "";
                this.config.map(x => {
                    if (x.level === level) {
                        path = x.path;
                        return;
                    }
                });
                return path;
            },
            save() {
                console.log(this.command);
                // this.$refs.form.validate(valid => {
                //     if (valid) {
                this.loading = true;
                const resopne = this.command.id
                    ? axios.put(this.getPath(this.command.level), this.command)
                    : axios.post(this.getPath(this.command.level), this.command);
                resopne.then(
                    response => {
                        this.loading = false;
                        this.$message({
                            type: "success",
                            message: "保存成功!"
                        });
                        this.search();
                    },
                    response => {
                        this.loading = false;
                        const result = response.response.data;
                        this.$message({
                            type: "error",
                            message: "保存失败:" + result.error,
                            center: true
                        });
                    }
                );
                //     } else {
                //         return false;
                //     }
                // });
            },

            back() {
                // this.$router.go(-1);
                this.dialogFormVisible = false;
            }
        }
    };
</script>
<style>
    .custom-tree-node {
        flex: 1;
        display: flex;
        align-items: center;
        justify-content: space-between;
        font-size: 12px;
        padding-right: 8px;
        border-top: none;
        border-bottom: 0.5px dashed #e6e6e6;
    }
</style>
