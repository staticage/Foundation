<template>
    <section class="editWorkflow">
        <el-form ref="form" :rules="rules" :model="command" label-width="100px">
            <el-row :gutter="10" class="nameType">
                <el-col :span="5">
                    <el-form-item label="工作流名称：" prop="name">
                        <el-input v-model="command.name" placeholder="请输入工作流名称"></el-input>
                    </el-form-item>
                </el-col>
                <el-col :span="5" style="padding-left: 1px;">
                    <el-form-item label="工作流类型：" prop="category">
                        <el-select
                                v-model="command.type"
                                placeholder="请选择类型"
                                @change="selectType"
                                :disabled="command.steps.length > 0"
                        >
                            <el-option
                                    v-for="(item,index) in typeList"
                                    :key="index"
                                    :label="item.type"
                                    :value="item.type"
                            ></el-option>
                        </el-select>
                    </el-form-item>
                </el-col>
                <el-col :span="5" style="padding-left: 1px;">
                    <el-form-item label="公寓：" prop="apartment">
                        <el-select
                                v-model="command.apartmentId"
                                placeholder="请选择公寓"
                        >
                            <el-option
                                    v-for="(apartment,index) in apartments"
                                    :key="index"
                                    :label="apartment.name"
                                    :value="apartment.id"
                            ></el-option>
                        </el-select>
                    </el-form-item>
                </el-col>
                <el-col :span="2" v-if="command.name && command.type">
                    <div style="cursor:pointer;margin-top: 5px;float:left" @click="addStep">
                        <i
                                class="el-icon-circle-plus-outline"
                                style="font-size: 24px;color:#409EFF"
                        ></i>
                    </div>
                </el-col>
            </el-row>
        </el-form>
        <el-row style="height: 1px;background-color: #999999;margin-bottom: 20px"></el-row>

        <el-row v-for="(item, index) in command.steps" :key="index">
            <el-col :span="5">
                <div class="chooseTypeBox" style>
                    <span class="chooseType">选择步骤：</span>
                    <el-select v-model="item.name" placeholder="选择步骤" class="chooseTypeTitle">
                        <el-option
                                v-for="(name,index) in stepTypes"
                                :key="index"
                                :label="name"
                                :value="name"
                        ></el-option>
                    </el-select>
                </div>
            </el-col>
            <el-col :span="19">
                <div class="chooseTypeBox">
                    <span class="chooseType">人员：</span>
                    <el-select v-model="item.roleId" placeholder="人员" @change="selectRole(item)">
                        <el-option
                                v-for="(role,uindex) in roles"
                                :key="uindex"
                                :label="role.name"
                                :value="role.id"
                        ></el-option>
                    </el-select>
                    <span
                            @click="upper(index)"
                            v-if="index !== 0"
                            style="cursor:pointer;margin-left: 10px;color: #666;"
                    >
                        <el-button type="primary" icon="el-icon-arrow-up">上移</el-button>
                    </span>
                    <span
                            @click="moveDown(index)"
                            v-if="index !== command.steps.length - 1"
                            style="cursor:pointer;margin-left: 10px;;color: #666;"
                    >
                        <el-button type="primary" icon="el-icon-arrow-down">下移</el-button>
                    </span>
                    <span
                            @click="deleteStepInfo(index)"
                            style="cursor:pointer;margin-left: 10px;;color: #666;"
                    >
                        <el-button type="danger" icon="el-icon-delete">删除</el-button>
                    </span>
                </div>
            </el-col>
        </el-row>
        <el-row style="margin-top: 50px;margin-bottom: 50px">
            <el-col :span="16">
                <el-steps>
                    <el-step
                            :title="item.name"
                            v-for="item in command.steps"
                            :description="item.roleName"
                            :key="item.id"
                    ></el-step>
                </el-steps>
            </el-col>
        </el-row>
        <el-row>
            <el-col :span="10" class="choosePeople">
                <span class="chooseType">选择人员：</span>
                <el-select v-model="selectedUsers" multiple placeholder="请选择" class="chooseTypeTitle"
                           style="width: 82%">
                    <el-option style="width: 100%;"
                               v-for="item in users"
                               :key="item.id"
                               :label="item.name"
                               :value="item.id">
                    </el-option>
                </el-select>
            </el-col>
        </el-row>
        <el-row class="button-bottom-row operation_btn">
            <el-button type="primary" :loading="saving" @click="edit">确认</el-button>
            <el-button @click="goBack">取消</el-button>
        </el-row>
    </section>
</template>
<script>
    import api from "../../../config/api";

    export default {
        data() {
            return {
                typeList: [],
                stepTypes: [],
                roles: [],
                stepList: [],
                typeValue: "",
                command: {
                    steps: []
                },
                rules: {
                    name: [
                        {
                            required: true,
                            message: "请输入工作流名称",
                            trigger: "blur"
                        }
                    ],
                    category: [
                        {validator: this.validateWorkflowType, trigger: "change"}
                    ]
                },
                saving: false,
                users: [],
                selectedUsers: [],
                apartments:[],
                apartment:""
            };
        },

        mounted() {
            const {id} = this.$route.params;
            this.command.id = id;
            this.$axios.get(api.workflowList).then(response => {
                this.typeList = response.data;
            }).catch(err => {
            });

            this.$axios.get(api.role + "/select-list").then(response => {
                this.roles = response.data;
                this.$axios.get(api.usersForWorkflow).then(res1 => {
                    this.users = res1.data;
                    this.$axios.get(api.myApartments).then(res3 => {
                        this.apartments = res3.data;
                        this.$axios.get(api.workflow + "/" + id).then(res2 => {
                            let data = res2.data;
                            data.steps = data.steps.map(x => {
                                let roleId = x.command.roleId;
                                let roleName = _.find(
                                    this.roles,
                                    x => x.id == roleId
                                ).name;
                                return {...x, roleId: roleId, roleName: roleName};
                            });
                            this.command = data;
                            this.selectedUsers = data.forwardingUsers;
                            this.stepTypes = _.find(
                                this.typeList,
                                x => x.type === this.command.type
                            ).items;
                        }).catch(err => {
                        });
                    }).catch(err => {
                    });
                }).catch(err => {
                });
            }).catch(err => {
            });
        },

        methods: {
            upper(index) {
                let current = this.command.steps[index];
                let prev = this.command.steps[index - 1];

                this.$set(this.command.steps, index, {...prev, id: current.id});
                this.$set(this.command.steps, index - 1, {
                    ...current,
                    id: prev.id
                });
            },
            moveDown(index) {
                let current = this.command.steps[index];
                let prev = this.command.steps[index + 1];

                this.$set(this.command.steps, index, {...prev, id: current.id});
                this.$set(this.command.steps, index + 1, {
                    ...current,
                    id: prev.id
                });
            },

            deleteStepInfo(index) {
                this.command.steps.splice(index, 1);
            },

            addStep() {
                const isNull = _.some(
                    this.command.steps,
                    x => x.name == null || x.roleId == null
                );
                if (isNull) {
                    this.$message.error("请完成一个步骤后再继续");
                    return;
                }
                if (this.command.steps.length > 1) {
                    const last = this.command.steps[this.command.steps.length - 1];
                    let isDuplicate = false;
                    for (let i = 0; i < this.command.steps.length - 1; i++) {
                        if (
                            this.command.steps[i].name == last.name &&
                            this.command.steps[i].roleId == last.roleId
                        ) {
                            isDuplicate = true;
                        }
                    }
                    if (isDuplicate) {
                        this.$message.error("不能添加一样的步骤");
                        return;
                    }
                }
                let obj = {
                    name: null,
                    roleId: null,
                    id: new Date().getTime()
                };
                this.command.steps.push(obj);
            },

            selectType() {
                this.stepTypes = _.find(
                    this.typeList,
                    x => x.type === this.command.type
                ).items;
            },

            selectRole(item) {
                item.roleName = _.find(this.roles, x => x.id == item.roleId).name;
            },

            validateWorkflowType(rule, value, callback) {
                if (!this.command.type) {
                    callback(new Error("请选择工作流类型"));
                } else {
                    callback();
                }
            },

            typeChange(val) {
                this.disabledType = val === 2 ? true : false;
            },

            edit() {
                this.$refs.form.validate(valid => {
                    if (valid) {
                        if (this.command.steps.length == 0) {
                            this.$message.error("请至少添加一个步骤");
                            return;
                        }
                        const isNull = _.some(
                            this.command.steps,
                            x => x.name == null || x.roleId == null
                        );
                        if (isNull) {
                            this.$message.error("请删除未完成的步骤再进行创建");
                            return;
                        }

                        const last = this.command.steps[
                        this.command.steps.length - 1
                            ];
                        let isDuplicate = false;
                        for (let i = 0; i < this.command.steps.length - 1; i++) {
                            if (
                                this.command.steps[i].name == last.name &&
                                this.command.steps[i].roleId == last.roleId
                            ) {
                                isDuplicate = true;
                            }
                        }
                        if (isDuplicate) {
                            this.$message.error("请删除重复的步骤再进行创建");
                            return;
                        }
                        this.saving = true;
                        this.$axios
                            .put(api.workflow, this.command)
                            .then(response => {
                                this.saving = false;
                                this.$message({
                                    message: "修改成功",
                                    type: "success"
                                });
                                this.$router.go(-1);
                            })
                            .catch(err => {
                                this.saving = false;
                                this.$message({
                                    message: "修改失败：" + err.message,
                                    type: "error"
                                });
                            });
                    } else {
                        return false;
                    }
                });
            },

            goBack() {
                this.$router.go(-1);
            }
        }
    };
</script>
<style lang="scss">
    .editWorkflow {
        @media screen and (max-width: 1366px) {
            .el-input--small .el-input__inner {
                width: 140px;
            }
        }
        @media screen and (min-width: 1920px) {
            .el-input--small .el-input__inner {
                width: 200px;
            }
        }
    }

</style>
<style scoped>
    .chooseType {
        text-align: right;
        vertical-align: middle;
        float: left;
        font-size: 12px;
        color: #606266;
        line-height: 33px;
        padding: 0 12px 0 0;
        -webkit-box-sizing: border-box;
        box-sizing: border-box;
        height: 50px;
    }

    .stepBox {
        float: left;
        width: 60%;
        color: #333;
        margin-top: 20px;
    }

    .el-step__title {
        font-size: 14px;
    }

    .chooseTypeTitle {
        margin-left: 10px;
    }

    .nameType {
        width: 100%;
    }
</style>
<style>
    .el-step__title.is-wait,
    .el-step__description.is-wait {
        color: #333;
    }

    .el-step__icon {
        background: #73dacf;
    }

    .el-step__icon.is-text {
        border: 1px solid #73dacf;
    }

    .el-step__icon-inner {
        color: #fff;
    }

    .choosePeople .el-input--small .el-input__inner {
        width: 100%;
    }
</style>
