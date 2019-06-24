<template>
    <section>
        <el-row class="button-top-row" v-if="$hasPermission('新增')">
            <el-button-group>
                <el-button type="primary" size="mini" @click="create" icon="el-icon-circle-plus">新增</el-button>
            </el-button-group>
        </el-row>
        <el-row class="search-row el-form el-form--inline">
            <el-col :span="24">
                <label>用户名/姓名：</label>
                <el-input v-model="query.accountAndName" placeholder="用户名/姓名"></el-input>
                <label>电话：</label>
                <el-input v-model="query.phone" placeholder="电话"></el-input>
                <label>是否启用：</label>
                <div class="filter-content" style="height:auto">
                    <SingleSelector :data="meetingStatus" v-model="query.enabled"  @selected="newSearch"></SingleSelector>
                </div>
                <!--<el-select v-model="query.enabled" placeholder="请选择启用状态">-->
                    <!--<el-option value label="        全部"></el-option>-->
                    <!--<el-option value="true" label=" 启用"></el-option>-->
                    <!--<el-option value="false" label="禁用"></el-option>-->
                <!--</el-select>-->
                <el-button type="primary" icon="el-icon-search" @click="search">查询</el-button>
            </el-col>
        </el-row>
        <el-row class="table-row">
            <el-table :data="resource.items">
                <el-table-column prop="account" label="用户名"></el-table-column>
                <el-table-column prop="name" label="姓名"></el-table-column>
                <el-table-column prop="phone" label="电话"></el-table-column>
                <el-table-column prop="apartmentNames" label="所属公寓">
                    <template slot-scope="scope">
                        <el-tag style="margin-right:5px;" v-for="apartmentName in scope.row.apartmentNames"
                                :key="apartmentName" type disable-transitions>{{apartmentName}}
                        </el-tag>
                        <!-- {{scope.row.apartmentNames ? scope.row.apartmentNames.join(",") : ""}} -->
                    </template>
                </el-table-column>
                <el-table-column prop="roleNames" label="所属角色">
                    <template slot-scope="scope">
                        <el-tag style="margin-right:5px;" v-for="roleName in scope.row.roleNames" :key="roleName"
                                type="info" disable-transitions>{{roleName}}
                        </el-tag>
                        <!-- {{scope.row.apartmentNames ? scope.row.apartmentNames.join(",") : ""}} -->
                    </template>
                </el-table-column>

                <el-table-column prop="enabled" label="状态">
                    <template slot-scope="scope">
                        <el-tag :type="scope.row.enabled === true ? 'success' : 'danger'" disable-transitions>
                            {{scope.row.enabled === true ? '启用' : '禁用'}}
                        </el-tag>
                    </template>
                </el-table-column>
                <el-table-column fixed="right" label="操作">
                    <template slot-scope="scope">
                        <el-button @click="resetPassword(scope.row.id)" type="text">重置</el-button>
                        <el-button @click="edit(scope.row)" type="text" v-if="$hasPermission('编辑')">编辑</el-button>
                        <el-button @click="del(scope.row.id)" type="text" v-if="$hasPermission('删除')">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
            <el-pagination :current-page="query.page" :page-sizes="[10, 20, 30, 40]" :page-size="query.pageSize"
                           layout="total,prev,pager,next,sizes,jumper" :total="resource.total"
                           @size-change="handleSizeChange" @current-change="handleCurrentChange"></el-pagination>
        </el-row>
        <el-dialog :close-on-click-modal="false" title="用户信息" :visible.sync="dialogFormVisible" width="30%">
            <el-form ref="form" :rules="rules" :model="command" label-width="80px">
                <el-form-item label="用户名" prop="account">
                    <el-input v-model="command.account" placeholder="请输入用户名"></el-input>
                </el-form-item>
                <el-form-item label="角色" prop="roleIds" v-if="!command.id || !command.companyAdmin">
                    <el-checkbox style="margin-right:10px;" v-model="command.companyAdmin"
                                 v-if="!command.id || command.companyAdmin">管理员
                    </el-checkbox>
                    <el-select v-model="command.roleIds" placeholder="请选择角色" v-if="!command.companyAdmin"
                               style="width:100%" multiple>
                        <el-option v-for="role in roles" :key="role.id" :label="role.name" :value="role.id"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="所属项目" prop="apartments">
                    <el-select v-model="command.apartments" multiple placeholder="请选择">
                        <el-option v-for="apartment in apartments" :key="apartment.id" :label="apartment.name"
                                   :value="apartment.id"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="姓名" prop="name">
                    <el-input v-model="command.name" placeholder="请输入姓名"></el-input>
                </el-form-item>
                <el-form-item label="性别" prop="gender">
                    <el-radio-group v-model="command.gender">
                        <el-radio :label="1">男</el-radio>
                        <el-radio :label="2">女</el-radio>
                    </el-radio-group>
                </el-form-item>
                <el-form-item label="联系电话" prop="phone">
                    <el-input v-model="command.phone" placeholder="请输入联系电话"></el-input>
                </el-form-item>
                <el-form-item label="电子邮箱" prop="email">
                    <el-input v-model="command.email" placeholder="请输入电子邮箱"></el-input>
                </el-form-item>
                <el-form-item label="是否启用">
                    <el-switch v-model="command.enabled"></el-switch>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="back">取 消</el-button>
                <el-button type="primary" @click="save">确 定</el-button>
            </div>
        </el-dialog>
    </section>
</template>
<script>
    import ElementUI from "element-ui";
    import api from "../../config/api";
    import _ from "lodash";
    import validators from "../../common/validators";
    import SingleSelector from "../../components/SingleSelector.vue";


    export default {
        components: {
            SingleSelector
        },
        data() {
            return {

                meetingStatus: [
                    {
                        value: null,
                        name: "全部"
                    },
                    {
                        value: 1,
                        name: "启用"
                    },
                    {
                        value: 0,
                        name: "禁用"
                    }],
                command: {},
                query: {
                    page: 1,
                    enabled:null
                },
                roles: [],
                adminRoleIds: [],
                resource: {},
                apartments: [],
                dialogFormVisible: false,
                loading: false,
                rules: {
                    account: [{required: true, message: "请输入用户名", trigger: "blur"}],
                    name: [{required: true, message: "请输入姓名", trigger: "blur"}],
                    gender: [{required: true, message: "请选择性别", trigger: "change"}],
                    phone: [
                        {validator: validators.phone, trigger: "blur"}
                    ],
                    email: [
                        {
                            type: "email",
                            message: "电子邮箱格式错误",
                            trigger: "blur"
                        }
                    ]
                    // roleIds: [
                    //     {
                    //         type: "array",
                    //         required: true,
                    //         validator: this.validateRoleIds,
                    //         message: "请选择角色",
                    //         trigger: "change"
                    //     }
                    // ]
                }
            };
        },

        async mounted() {
            // this.user = this.$store.state.user;
            let allRoles = (await this.$axios.get(api.role + "/select-list")).data;
            this.roles = _.filter(allRoles, function (o) {
                return !o.companyAdmin;
            });
            this.adminRoleIds = _.map(_.filter(allRoles, ["companyAdmin", true]), "id");
            this.search();
            this.apartments = (await this.$axios.get(api.apartmentDropdown)).data;
        },

        methods: {
            newSearch() {
                this.query.page = 1;
                this.search();
            },
            handleSizeChange(pageSize) {
                this.query.pageSize = pageSize;
                this.search();
            },

            handleCurrentChange(page) {
                this.query.page = page;
                this.search();
            },
            async search() {
                this.resource = (await this.$axios.post("user/query", this.query)).data;
                console.log(this.resource)
            },
            create() {
                this.command = {
                    id: "",
                    roleIds: [],
                    apartments: [],
                    originalPassword: "123456",
                    enabled: true,
                    companyAdmin: false,
                    gender: 1
                };
                this.dialogFormVisible = true;
                if (this.$refs["form"]) {
                    this.$refs["form"].clearValidate(); // 重置验证
                }
            },
            async del(id) {
                this.$confirm("确认删除?", "提示", {
                    confirmButtonText: "确定",
                    cancelButtonText: "取消",
                    type: "warning"
                })
                    .then(() => {
                        this.$axios.delete("user/" + id).then(
                            response => {
                                this.loading = false;
                                this.$message({
                                    type: "success",
                                    message: "删除成功!"
                                });
                                this.search();
                            }
                        ).catch(err => {
                            this.loading = false;
                            this.$message({
                                type: "error",
                                message: "删除失败:" + err.message,
                                center: true
                            });
                        });
                    })
                    .catch(() => {
                        // catch 不要删除，Uncaught (in promise) cancel
                    });
            },

            resetPassword(id) {
                this.$confirm("将密码重置为[123456]?", "提示", {
                    confirmButtonText: "确定",
                    cancelButtonText: "取消",
                    type: "warning"
                })
                    .then(() => {
                        this.$axios
                            .post("user/reset-password", {id, password: "123456"})
                            .then(
                                response => {
                                    this.loading = false;
                                    this.$message({
                                        type: "success",
                                        message: "重置成功!"
                                    });
                                    this.search();
                                },
                                response => {
                                    this.loading = false;
                                    const result = response.response.data;
                                    this.$message({
                                        type: "error",
                                        message: "重置失败:" + result.error,
                                        center: true
                                    });
                                }
                            );
                    })
                    .catch(() => {
                        // catch 不要删除，Uncaught (in promise) cancel
                    });
            },

            save() {
                this.$refs.form.validate(valid => {
                    if (valid) {
                        this.loading = true;
                        if (this.command.companyAdmin === true) {
                            this.command.roleIds = this.adminRoleIds;
                        }
                        const httpRequest = this.command.id
                            ? this.$axios.put("user", this.command)
                            : this.$axios.post("user", this.command);
                        httpRequest.then(
                            response => {
                                this.loading = false;
                                this.$message({
                                    message: "保存成功",
                                    type: "success"
                                });
                                this.dialogFormVisible = false;
                                this.search();
                                this.back();
                            },
                            err => {
                                this.loading = false;
                                const result = err.response.data;
                                this.$message({
                                    type: "error",
                                    message: "保存失败:" + result.Message
                                });
                            }
                        );
                    } else {
                        return false;
                    }
                });
            },

            async edit(row) {
                //   this.command = (await this.$axios.get("user/" + row.id)).data;
                //   console.log(row);
                //   console.log(this.command);
                this.command = _.cloneDeep(row);
                this.dialogFormVisible = true;
                if (this.$refs["form"]) {
                    this.$refs["form"].clearValidate(); // 重置验证
                }
            },

            back() {
                this.dialogFormVisible = false;
                //   this.command = { roleIds: [], apartments: [], companyAdmin: false };
            }
        }
    };
</script>
