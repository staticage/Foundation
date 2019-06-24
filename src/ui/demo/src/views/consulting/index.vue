<template>
    <section class="salesIndex">
        <el-row class="button-top-row">
            <el-button v-if="$hasPermission('新增')" type="primary" size="mini" @click="create" icon="el-icon-circle-plus">新增</el-button>
            <el-button v-if="$hasPermission('查看')" type="primary" size="mini" @click="transferAll" icon="el-icon-sort">转交</el-button>
            <el-button v-if="$hasPermission('查看')" type="primary" size="mini" @click="showFavoriteModel" icon="el-icon-star-on">收藏</el-button>
        </el-row>

        <el-row class="search-row el-form el-form--inline inputBox">
            <el-col :span="24">
                <label>关&nbsp;&nbsp;&nbsp;&nbsp;键&nbsp;&nbsp;&nbsp;字&nbsp;&nbsp;&nbsp;：</label>
                <div class="filter-content" style="height:auto">
                    <el-input v-model="query.numberOrName" placeholder="关键字"></el-input>
                </div>
                <label>录入开始：</label>
                <!--<div class="filter-content" style="height:auto">-->
                <!--<el-date-picker v-model="query.startTime" type="date" placeholder="开始时间"></el-date-picker>-->
                <!--<el-date-picker v-model="query.endTime" type="date" placeholder="结束时间"></el-date-picker>-->
                <!--</div>-->
                <div class="filter-content" style="height:auto">
                    <el-date-picker
                        style="width: 230px"
                        v-model="times"
                        type="daterange"
                        @change="getTime"
                        range-separator="-"
                        start-placeholder="开始日期"
                        end-placeholder="结束日期"
                    ></el-date-picker>
                </div>

                <label class="ml">公寓名称：</label>
                <div class="filter-content" style="height:auto">
                    <el-input v-model="query.apartmentName" placeholder="公寓名称"></el-input>
                </div>

                <el-button type="primary" @click="search" :loading="loading" icon="el-icon-search">查询</el-button>
                <el-button type="info" plain @click="reset" icon="el-icon-refresh">重置</el-button>
            </el-col>

            <el-col :span="24">
                <!--<label>客户级别：</label>-->
                <!--<ConfigurationSelector v-model="query.customerLevel" type="销售项配置" name="客户级别"></ConfigurationSelector>-->
                <label>跟&nbsp;&nbsp;踪&nbsp;&nbsp;方&nbsp;&nbsp;式：</label>
                <ConfigurationSelector v-model="query.traceType" type="销售项配置" name="追踪类型"></ConfigurationSelector>
                <label>跟踪时间：</label>
                <!--<div class="filter-content" style="height:auto">-->
                <!--<el-date-picker v-model="query.traceStartTime" type="date" placeholder="开始时间"></el-date-picker>-->
                <!--<el-date-picker v-model="query.traceEndTime" type="date" placeholder="结束时间"></el-date-picker>-->
                <!--</div>-->
                <div class="filter-content" style="height:auto">
                    <el-date-picker
                        style="width: 230px"
                        v-model="trackingTimes"
                        type="daterange"
                        @change="getTrackingTime"
                        range-separator="-"
                        start-placeholder="开始日期"
                        end-placeholder="结束日期"
                    ></el-date-picker>
                </div>
            </el-col>
        </el-row>

        <!--<SearchButton v-model="isMore"></SearchButton>-->

        <el-row class="table-row">
            <el-table ref="table" :data="resource.items" @selection-change="handleSelectionChange">
                <el-table-column type="selection" width="40"></el-table-column>
                <el-table-column label width="40">
                    <template slot-scope="scope">
                        <div style="cursor:pointer">
                            <el-tooltip class="item" effect="dark" content="已收藏" placement="top">
                                <i class="el-icon-star-on" v-if="scope.row.favorite" style="color: orange;font-size: 22px"></i>
                            </el-tooltip>
                        </div>
                    </template>
                </el-table-column>

                <el-table-column prop="number" label="客户编号"></el-table-column>
                <el-table-column prop="name" label="客户姓名"></el-table-column>
                <el-table-column prop="phone" label="客户电话"></el-table-column>
                <el-table-column prop="consultantName" label="咨询人名称"></el-table-column>
                <el-table-column prop="consultantPhone" label="咨询人电话"></el-table-column>
                <el-table-column prop="consultingTypeText" label="咨询状态"></el-table-column>
                <el-table-column prop="consultingStatusText" label="状态"></el-table-column>
                <el-table-column prop="sellerName" label="签约销售"></el-table-column>
                <!-- <el-table-column prop="g" label="销售人员"></el-table-column> -->
                <el-table-column label="操作" width="200">
                    <template slot-scope="scope">
                        <el-button @click="details(scope.row.id)" type="text">查看</el-button>
                        <el-button @click="contract(scope.row)" type="text" v-if="!scope.row.hasContract">签约</el-button>
                        <el-button @click="edit(scope.row.id)" type="text" v-if="$hasPermission('编辑')">编辑</el-button>
                        <el-button @click="showTransferDialog(scope.row.id,scope.row.sellerId)" type="text" v-if="$hasPermission('编辑')">转交</el-button>
                        <el-button @click="remove(scope.row.id)" type="text" v-if="$hasPermission('删除')">删除</el-button>
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
            title="添加到收藏"
            :visible.sync="favoriteDialogVisible"
            width="30%"
            @close="(favoriteDialogVisible = false), resetForm('favoriteForm')"
        >
            <el-form :model="favoriteForm" :rules="favoriteRules" ref="favoriteForm">
                <el-form-item label="收藏夹" prop="category">
                    <el-select v-model="selectedFavoriteCategory" filterable allow-create default-first-option placeholder="选择或新建收藏夹">
                        <el-option v-for="item in favoriteCategories" :key="item.value" :label="item.text" :value="item.value"></el-option>
                    </el-select>
                </el-form-item>
            </el-form>

            <div slot="footer" class="dialog-footer">
                <el-button @click="favoriteDialogVisible = false">取 消</el-button>

                <el-button type="primary" :loading="addingFavorite" @click="addToFavorite">确 定</el-button>
            </div>
        </el-dialog>

        <el-dialog
            :close-on-click-modal="false"
            title="客户转交"
            :visible.sync="transferDialogVisible"
            width="30%"
            @close="(transferDialogVisible = false),(isTransferAll = false),resetForm('transferForm')"
        >
            <el-form :model="transferForm" :rules="transferRules" ref="transferForm">
                <el-form-item label="转交给" prop="selectedUser">
                    <el-select v-model="transferForm.selectedUser" placeholder="请选择用户">
                        <el-option v-for="item in transferForm.users" :key="item.id" :label="item.name" :value="item.id"></el-option>
                    </el-select>
                </el-form-item>

                <el-form-item label="转交原因" prop="reason">
                    <el-input v-model="transferForm.reason" type="textarea" placeholder="请输入转交原因"></el-input>
                </el-form-item>
            </el-form>

            <div slot="footer" class="dialog-footer">
                <el-button @click="transferDialogVisible = false">取 消</el-button>

                <el-button type="primary" :loading="transfering" @click="transfer">确 定</el-button>
            </div>
        </el-dialog>
    </section>
</template>

<script>

import ConfigurationSelector from "../../components/ConfigurationSelector.vue";
import api from "../../config/api";

export default {
    components: {
        // SearchButton,
        ConfigurationSelector
    },

    data() {
        return {
            // isMore: false,
            times: '',
            trackingTimes: '',
            currentPage: 15,
            query: {},
            loading: false,
            isTransferAll: false,
            resource: {},
            favoriteDialogVisible: false,
            selectedFavoriteCategory: null,
            favoriteCategories: [],
            addToFavoriteCommand: {
                userId: this.$store.state.user.id
            },
            transferCommand: {
                fromUserId: this.$store.state.user.id
            },
            addingFavorite: false,
            favoriteForm: {},
            favoriteRules: {
                category: [{ validator: this.validateCategory, trigger: "change" }]
            },
            transferDialogVisible: false,
            transferForm: {
                users: []
            },
            transferRules: {
                selectedUser: [
                    { required: true, message: "请选择转交用户", trigger: "change" }
                ]
            },
            transfering: false,
            selectionItems: []
        };
    },

    async mounted() {
        this.search();
        // this.$axios
        //     .get(api.favoriteCategories)
        //     .then(response => {
        //         this.favoriteCategories = response.data;
        //     })
        //     .catch(err => {

        //         console.log(err.message);

        //     });

        // this.$axios

        //     .get(api.usersToBeTransfer)

        //     .then(response => {

        //         this.transferForm.users = response.data;

        //     })
        //     .catch(err => {
        //         console.log(err.message);
        //     });

    },

    methods: {
        getTime() {
            console.log(this.times, 'uiuiuiuiuiui')
            // let dateTime = this.times;
        },
        getTrackingTime() {

        },

        handleSelectionChange(val) {

            this.selectionItems = val;

        },

        handleSizeChange(pageSize) {
            console.log(this.query, 'this.query00000');

            this.query.pageSize = pageSize;

            this.search();

        },

        handleCurrentChange(page) {
            console.log(this.query, 'this.query111');
            this.query.page = page;

            this.search();

        },
        async search() {
            this.loading = true;
            // if (this.times !== '' || this.trackingTimes !== '') {
            //     this.query.traceStartTime = this.trackingTimes[0]
            //     this.query.traceEndTime = this.trackingTimes[1]
            //     this.query.startTime = this.times[0]
            //     this.query.endTime = this.times[1]
            // }
            // this.resource = (await this.$axios.post(
            //     "consulting/query",
            //     this.query
            // )).data;
            // console.log(this.query, 'this.query');
            this.loading = false;
        },
        reset() {
            this.query = {};
            this.times = [];
            this.trackingTimes = [];
        },
        create() {
            this.$router.push({
                name: "consulting-create"
            });
        },
        contract(row) {
            this.$router.push({
                name: "contract-create",
                params: { id: row.id }
            });
        },
        edit(id) {
            this.$router.push({
                name: "consulting-edit",
                params: { id }
            });
        },
        async remove(id) {
            try {
                this.$confirm("是否删除?", "提示", {
                    confirmButtonText: "确定",
                    cancelButtonText: "取消",
                    type: "warning"
                }).then(async () => {
                    await this.$axios.delete("consulting/" + id);
                    this.$message({
                        message: "删除成功",
                        type: "success"
                    });
                    var index = _.findIndex(this.resource.items, x => x.id == id);
                    this.resource.items.splice(index, 1);
                });
            } catch (error) {
                console.log(error);
            }
        },
        details(id) {
            this.$router.push({
                name: "consulting-details",
                params: { id }
            });
        },

        showFavoriteModel() {

            if (this.selectionItems.length == 0) {

                this.$message({

                    message: "请至少选择一条记录再进行收藏",

                    type: "error"

                });

                return;

            }

            this.selectedFavoriteCategory = null;

            this.favoriteDialogVisible = true;

        },

        addToFavorite() {

            this.$refs["favoriteForm"].validate(valid => {

                if (valid) {

                    this.addingFavorite = true;

                    this.addToFavoriteCommand.category = this.selectedFavoriteCategory;

                    this.addToFavoriteCommand.consultingIds = this.selectionItems.map(x => x.id);

                    this.$axios.post(api.addToFavorite, this.addToFavoriteCommand).then(response => {

                        this.$message({

                            message: "收藏成功",

                            type: "success"

                        });

                        var matched = this.resource.items.filter(x => _.includes(this.addToFavoriteCommand.consultingIds, x.id));

                        matched.forEach(x => x.favorite = true);

                        this.favoriteDialogVisible = false;

                        this.addingFavorite = false;

                        this.$refs.table.clearSelection();

                    }).catch(err => {

                        this.$message({

                            message: "收藏失败: " + err.message,

                            type: "error"

                        });

                        this.favoriteDialogVisible = false;

                        this.addingFavorite = false;

                        this.$refs.table.clearSelection();

                    });

                } else {

                    console.log("error submit!!");

                    return false;

                }

            });

        },

        transferAll() {

            if (this.selectionItems.length == 0) {

                this.$message({

                    message: "请至少选择一条记录再进行转交",

                    type: "error"

                });

                return;

            }

            this.transferDialogVisible = true;

            this.isTransferAll = true;

        },

        transfer() {

            this.$refs["transferForm"].validate(valid => {

                if (valid) {

                    this.transfering = true;

                    this.transferCommand.toUserId = this.transferForm.selectedUser;

                    this.transferCommand.reason = this.transferForm.reason;

                    var url = api.transferConsulting;

                    if (this.isTransferAll) {

                        url = api.transferMultipleConsulting;

                        this.transferCommand.pairs = this.selectionItems.map(x => {

                            return {

                                fromUserId: x.sellerId,

                                consultingId: x.id

                            }

                        })

                    }

                    this.$axios.post(url, this.transferCommand).then(response => {

                        this.$message({

                            message: "转交成功",

                            type: "success"

                        });

                        this.isTransferAll = false;

                        this.transferDialogVisible = false;

                        this.transfering = false;

                        this.search();

                    }).catch(err => {

                        this.$message({

                            message: "转交成功: " + err.message,

                            type: "error"

                        });

                        this.isTransferAll = false;

                        this.transferDialogVisible = false;

                        this.transfering = false;

                    });

                } else {

                    console.log("error submit!!");

                    return false;

                }

            });

        },

        resetForm(name) {

            this.$refs[name].resetFields();

        },

        showTransferDialog(id, fromUserId) {

            this.transferDialogVisible = true;

            this.transferCommand.consultingId = id;

            this.transferCommand.fromUserId = fromUserId;

        },

        validateCategory(rule, value, callback) {

            if (!this.selectedFavoriteCategory) {

                callback(new Error("请选择或创建一个收藏夹"));

            } else {

                callback();

            }

        }

    }

};

</script>

