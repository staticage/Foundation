<template>
    <section>
        <el-row class="button-top-row" v-if="$hasPermission('新增')">
            <el-button-group>
                <el-button type="primary" size="mini" @click="create" icon="el-icon-circle-plus">新增</el-button>
            </el-button-group>
        </el-row>
        <el-row class="search-row el-form el-form--inline">
            <el-col :span="24">
                <label>所属公寓：</label>
                <el-select v-model="query.apartmentId" placeholder="请选择所属公寓">
                    <el-option v-for="item in apartments" :key="item.id" :label="item.name"
                               :value="item.id"></el-option>
                </el-select>
                <label>服务类别：</label>
                <ConfigurationSelector v-model="query.type" type="销售项配置" name="服务类别"></ConfigurationSelector>
                <label>服务名称：</label>
                <el-input v-model="query.name" placeholder="请输入服务名称"></el-input>
                <el-button type="primary" icon="el-icon-search" @click="search">查询</el-button>
            </el-col>
        </el-row>
        <el-row class="table-row">
            <el-table :data="resource.items">
                <el-table-column prop="apartmentName" label="所属公寓"></el-table-column>
                <!-- <el-table-column prop="code" label="服务编码"></el-table-column> -->
                <el-table-column prop="name" label="服务名称"></el-table-column>
                <el-table-column prop="typeText" label="服务类别"></el-table-column>
                <el-table-column prop="price" label="单次价格"></el-table-column>
                <el-table-column prop="description" label="服务说明"></el-table-column>
                <el-table-column fixed="right" label="操作">
                    <template slot-scope="scope">
                        <el-button @click="edit(scope.row)" type="text" v-if="$hasPermission('编辑')">编辑</el-button>
                        <el-button @click="del(scope.row)" type="text" v-if="$hasPermission('删除')">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
            <el-pagination
                    :current-page="query.page"
                    :page-sizes="[10, 20, 30, 40]"
                    :page-size="query.pageSize"
                    layout="total,prev,pager,next,sizes,jumper"
                    :total="resource.total"
                    @size-change="handleSizeChange"
                    @current-change="handleCurrentChange"
            ></el-pagination>
            <!-- @size-change="handleSizeChange"
            @current-change="handleCurrentChange"-->
        </el-row>
        <el-dialog
                :close-on-click-modal="false"
                title="服务项目"
                :visible.sync="dialogFormVisible"
                width="30%"
        >
            <el-form ref="form" :rules="rules" :model="command" label-width="80px">
                <el-form-item label="所属公寓" prop="apartmentId">
                    <el-select
                            v-model="command.apartmentId"
                            :disabled="isEdit"
                            style="width: 100%;"
                            placeholder="请选择所属公寓"
                    >
                        <el-option
                                v-for="item in apartments"
                                :key="item.id"
                                :label="item.name"
                                :value="item.id"
                        ></el-option>
                    </el-select>
                </el-form-item>
                <!-- <el-form-item label="服务编码" :disabled="command.id?false:true">
                            <el-input v-model="command.code" placeholder="请输入服务编码"></el-input>
                </el-form-item>-->
                <el-form-item label="服务名称" prop="name">
                    <el-input v-model="command.name" placeholder="请输入服务名称"></el-input>
                </el-form-item>
                <el-form-item label="单次价格" prop="price">
                    <el-input-number
                            style="width: 100%;"
                            v-model="command.price"
                            controls-position="right"
                            :min="1"
                            :precision="2"
                            label="请输入单次价格"
                    ></el-input-number>
                </el-form-item>
                <el-form-item label="价格说明">
                    <el-input v-model="command.priceDescription" placeholder="请输入价格说明"></el-input>
                </el-form-item>
                <el-form-item label="服务类别" prop="type">
                    <!--<el-select v-model="command.type" style="width: 100%;" placeholder="请选择类型">-->
                    <!--<el-option label="未分类" :value="0"></el-option>-->
                    <!--<el-option label="生活照料" :value="1"></el-option>-->
                    <!--<el-option label="日常生活" :value="2"></el-option>-->
                    <!--<el-option label="医护服务" :value="3"></el-option>-->
                    <!--<el-option label="康复服务" :value="4"></el-option>-->
                    <!--<el-option label="点餐服务" :value="5"></el-option>-->
                    <!--<el-option label="电话服务" :value="6"></el-option>-->
                    <!--<el-option label="健康管理" :value="7"></el-option>-->
                    <!--<el-option label="餐饮服务" :value="8"></el-option>-->
                    <!--<el-option label="生活服务" :value="9"></el-option>-->
                    <!--<el-option label="安心保障" :value="10"></el-option>-->
                    <!--<el-option label="诊疗" :value="11"></el-option>-->
                    <!--<el-option label="个性服务" :value="12"></el-option>-->
                    <!--<el-option label="其他服务" :value="13"></el-option>-->
                    <!--</el-select>-->
                    <ConfigurationSelector v-model="command.type" type="销售项配置" name="服务类别"
                                           style="width:100%;"></ConfigurationSelector>
                </el-form-item>
                <el-form-item label="服务说明">
                    <el-input v-model="command.description" type="textarea" placeholder="请输入服务说明"></el-input>
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
    import _ from "lodash";
    import api from "../../../config/api.js";
    import ConfigurationSelector from "../../../components/ConfigurationSelector.vue";

    export default {
        components: {ConfigurationSelector},
        data() {
            return {
                command: {},
                query: {page: 1},
                resource: {},
                dialogFormVisible: false,
                loading: false,
                apartments: [],
                isEdit: false,
                rules: {
                    // account: [{ required: true, message: "请输入用户名", trigger: "blur" }],
                    // name: [{ required: true, message: "请输入姓名", trigger: "blur" }],
                    // gender: [{ required: true, message: "请选择性别", trigger: "change" }],
                    // phone: [{ required: true, message: "请输入联系电话", trigger: "blur" }],
                    // roleIds: [{ type: "array", required: true, message: "请选择角色", trigger: "change" }]
                },
                rules: {
                    apartmentId: [
                        {required: true, message: "请选择所属公寓", trigger: "change"}
                    ],
                    name: [{required: true, message: "请输入服务名称", trigger: "blur"}],
                    type: [{required: true, message: "请选择类型", trigger: "change"}],
                    price: [
                        {required: true, message: "请输入单次价格", trigger: "change"}
                    ]
                }
            };
        },

        async mounted() {
            this.apartments = (await this.$axios.get(api.apartment + "/select-list")).data;
            this.search();
        },
        methods: {
            handleSizeChange(pageSize) {
                this.query.pageSize = pageSize;
                this.search();
            },

            handleCurrentChange(page) {
                this.query.page = page;
                this.search();
            },
            async search() {
                this.resource = (await this.$axios.post(
                    api.appreciationService + "/query",
                    this.query
                )).data;
            },

            create() {
                this.command = {id: ""};
                this.dialogFormVisible = true;
                this.isEdit = false;
            },

            async del(row) {
                this.$confirm("确认删除?", "提示", {
                    confirmButtonText: "确定",
                    cancelButtonText: "取消",
                    type: "warning"
                })
                    .then(() => {
                        this.$axios.delete(api.appreciationService + "/" + row.id).then(
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
            save() {
                this.$refs.form.validate(valid => {
                    if (valid) {
                        this.loading = true;
                        const httpRequest = this.command.id
                            ? this.$axios.put(api.appreciationService, this.command)
                            : this.$axios.post(api.appreciationService, this.command);
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
                            }
                        ).catch(err => {
                            this.loading = false;
                            this.$message({
                                type: "error",
                                message: "保存失败:" + err.message
                            });
                        });
                    } else {
                        return false;
                    }
                });
            },
            async edit(row) {
                this.command = _.clone(row);
                this.dialogFormVisible = true;
                this.isEdit = true;
            },

            back() {
                this.dialogFormVisible = false;
                this.command = {roleIds: []};
                if (this.$refs.form) {
                    this.$refs.form.resetFields();
                }
            }
        }
    };
</script>
