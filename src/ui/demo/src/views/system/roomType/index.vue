<template>
    <section>
        <el-row class="button-top-row" v-if="$hasPermission('新增')">
            <el-button-group>
                <el-button type="primary" size="mini" @click="create" icon="el-icon-circle-plus">新增</el-button>
            </el-button-group>
        </el-row>
        <el-row class="search-row el-form el-form--inline">
            <el-col :span="24">
                <label>房型名称：</label>
                <el-input v-model="query.name" placeholder="房型名称"></el-input>
                <label>公寓名称：</label>
                <el-select v-model="query.apartmentId" placeholder="请选择公寓名称">
                    <el-option label="全部" value></el-option>
                    <el-option v-for="item in apartments" :key="item.id" :label="item.name"
                               :value="item.id"></el-option>
                </el-select>
                <el-button type="primary" icon="el-icon-search" @click="search()">查询</el-button>
                <!-- <el-button type="info" plain @click="query={}">重置</el-button> -->
            </el-col>
        </el-row>
        <el-row class="table-row">
            <el-table :data="resource.items">
                <el-table-column prop="name" label="房型名称"></el-table-column>
                <el-table-column prop="apartmentName" label="公寓名称"></el-table-column>
                <el-table-column prop="typeText" label="房屋类型"></el-table-column>
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
        </el-row>
        <el-dialog
                :close-on-click-modal="false"
                title="房型信息"
                :visible.sync="dialogVisible"
                width="30%"
        >
            <el-form ref="form" :rules="rules" :model="command" label-width="80px">
                <el-form-item label="公寓" prop="apartmentId" v-if="!isEdit">
                    <el-select v-model="command.apartmentId" style="width: 100%;" placeholder="请选择公寓"
                               @change="checkPricePackage">
                        <el-option
                                v-for="item in apartments"
                                :key="item.id"
                                :label="item.name"
                                :value="item.id"
                        ></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="房型名称" prop="apartmentNames">
                    <el-input v-model="command.name" placeholder="请输入房型名称"></el-input>
                </el-form-item>
                <el-form-item label="类型" prop="type">
                    <el-select v-model="command.type" style="width: 100%;" placeholder="请选择类型">
                        <el-option label="单人间" :value="1"></el-option>
                        <el-option label="双人间" :value="2"></el-option>
                        <el-option label="三人间" :value="3"></el-option>
                        <el-option label="四人间" :value="4"></el-option>
                        <el-option label="套间" :value="5"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="价格" v-if="isPricePackage" prop="price">
                    <el-input-number v-model="command.price" controls-position="right" :min="0" :precision="2"
                                     label="请输入价格" placeholder="请输入价格" style="width:100%;"></el-input-number>
                </el-form-item>
                <!-- <el-form-item label="床位数">
                            <el-input-number
                                v-model="command.bedsCount"
                                controls-position="right"
                                :min="1"
                                :max="20"
                                :precision="0"
                                label="请输入床位数"
                            ></el-input-number>
                </el-form-item>-->
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="back">取 消</el-button>
                <el-button type="primary" @click="save()">确 定</el-button>
            </div>
        </el-dialog>
    </section>
</template>
<script lang="ts">
    import ElementUI from "element-ui";
    import api from "../../../config/api";
    import axios from "../../../utils/http";
    import _ from "lodash";

    export default {
        components: {},

        data() {
            return {
                command: {},
                query: {page: 1},
                dialogVisible: false,
                apartments: [],
                loading: false,
                resource: [],
                isPricePackage: false,
                isEdit: false,
                rules: {
                    apartmentId: [
                        {required: true, message: "请选择公寓名称", trigger: "change"}
                    ],
                    apartmentNames: [
                        {required: true, validator: this.validateName, trigger: "blur"}
                    ],
                    type: [{required: true, message: "请选择类型", trigger: "change"}],
                    price: [{required: true, message: " 请输入价格", trigger: "blur"}]
                }
            };
        },

        async mounted() {
            this.apartments = (await axios.get("apartment/select-list")).data;
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
                this.resource = (await axios.post("roomtype/query", this.query)).data;
            },
            validateName(rule, value, callback) {
                if (!this.command.name) {
                    if (!this.command.consultantName) {
                        callback(new Error("请输入房型名称"));
                    } else {
                        callback();
                    }
                } else {
                    callback();
                }
            },
            checkPricePackage(val) {
                const apartment = this.apartments.find(x => x.id == val);
                this.isPricePackage = apartment.isPricePackage;
            },
            save() {
                this.$refs.form.validate(valid => {
                    if (valid) {
                        this.loading = true;
                        const httpRequest = this.command.id
                            ? axios.put("roomtype/edit", this.command)
                            : axios.post("roomtype/create", this.command);
                        httpRequest.then(
                            response => {
                                this.loading = false;
                                this.$message({
                                    message: "保存成功",
                                    type: "success"
                                });
                                this.dialogVisible = false;
                                this.search();
                                this.back();
                            }
                        ).catch(err => {
                            this.loading = false;
                            this.$message({
                                type: "error",
                                message: "保存失败:" + err.message,
                                showClose: true,
                                duration: 0
                            });
                        });
                    } else {
                        return false;
                    }
                });
            },

            create() {
                this.command = {id: ""};
                this.isEdit = false;
                this.dialogVisible = true;
                if (this.$refs["form"]) {
                    this.$refs["form"].clearValidate(); // 重置验证
                }
            },

            async edit(row) {
                this.command = _.clone(row);
                this.isEdit = true;
                this.dialogVisible = true;
                if (this.$refs["form"]) {
                    this.$refs["form"].clearValidate(); // 重置验证
                }
            },

            async del(row) {
                this.$confirm("确认删除?", "提示", {
                    confirmButtonText: "确定",
                    cancelButtonText: "取消",
                    type: "warning"
                })
                    .then(() => {
                        axios.delete("roomtype/" + row.id).then(response => {
                            this.loading = false;
                            this.$message({
                                type: "success",
                                message: "删除成功!"
                            });
                            this.search();
                        }).catch(err => {
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

            back() {
                this.$refs["form"].clearValidate(); // 重置验证
                this.command = {};
                this.dialogVisible = false;
                //   this.$refs["form"].resetFields();
            }
        }
    };
</script>
