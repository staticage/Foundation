<style lang="scss">
    .salesIndex {
        .inputBox {
            @media screen and (max-width: 1366px) {
                .el-input--small, .el-select > .el-input {
                    /*width: 178px;*/
                }
                .el-date-editor.el-input {
                    width: 126px;
                }
                .search-row .el-select {
                    width: 128px;
                }
            }
            @media screen and (min-width: 1920px) {
                .el-input--small, .el-range-editor--small.el-input__inner {
                    width: 178px;
                }
                .el-date-editor.el-input {
                    width: 178px;
                }
            }
        }
    }
</style>
<style lang="scss" scoped>

</style>
<template>
    <section class="salesIndex">
        <el-row class="button-top-row">
                <el-button  type="primary" size="mini" :loading="removingFavorite" @click="removeFavorite" icon="el-icon-circle-close">取消</el-button>
                <el-button  type="primary" size="mini" @click="transferAll" icon="el-icon-sort">转交</el-button>
        </el-row>
        <el-row class="search-row el-form el-form--inline inputBox">
            <el-col :span="24">
                <label>收藏夹:</label>
                <el-select v-if="favoriteCategories" v-model="category" @change="handleCategoryChange">
                    <el-option key="all" label="全部" value="all"></el-option>
                    <el-option v-for="item in favoriteCategories" :key="item.value" :label="item.text" :value="item.value">
                        {{item.text}}
                    </el-option>
                </el-select>
            </el-col>
        </el-row>
        <el-row class="table-row">
            <el-table ref="table" :data="consultingList" @selection-change="handleSelectionChange">
                <el-table-column type="selection" width="55"></el-table-column>
                <el-table-column prop="number" label="客户编号"></el-table-column>
                <el-table-column prop="name" label="客户姓名"></el-table-column>
                <el-table-column prop="phone" label="客户电话"></el-table-column>
                <el-table-column prop="consultantName" label="咨询人名称"></el-table-column>
                <el-table-column prop="consultantPhone" label="咨询人电话"></el-table-column>
                <el-table-column prop="consultingTypeText" label="咨询状态"></el-table-column>
                <el-table-column prop="consultingStatusText" label="状态"></el-table-column>
                <el-table-column label="操作">
                    <template slot-scope="scope">
                        <el-button @click="details(scope.row.id)" type="text">查看</el-button>
                        <el-button @click="contract(scope.row)" type="text" v-if="!scope.row.hasContract">签约</el-button>
                        <el-button @click="edit(scope.row.id)" type="text" v-if="$hasPermission('编辑')">编辑</el-button>
                        <el-button @click="showTransferDialog(scope.row.id,scope.row.sellerId)" type="text" v-if="$hasPermission('编辑')">转交</el-button>
                        <el-button @click="remove(scope.row.id)" type="text" v-if="$hasPermission('删除')">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </el-row>
        <el-dialog :close-on-click-modal="false" title="客户转交" :visible.sync="transferDialogVisible" width="30%"
                   @close="transferDialogVisible=false,isTransferAll=false,resetForm('transferForm')">
            <el-form :model="transferForm" :rules="transferRules" ref="transferForm">
                <el-form-item label="转交给" prop="selectedUser">
                    <el-select v-model="transferForm.selectedUser" placeholder="请选择用户">
                        <el-option
                                v-for="item in transferForm.users"
                                :key="item.id"
                                :label="item.name"
                                :value="item.id"
                        ></el-option>
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
    import SearchButton from "../../components/SearchButton.vue";
    import ConfigurationSelector from "../../components/ConfigurationSelector.vue";
    import api from "../../config/api";

    export default {
        components: {
            SearchButton,
            ConfigurationSelector
        },
        data() {
            return {
                category: "all",
                currentPage: 15,
                query: {},
                loading: false,
                consultingList: [],
                favoriteDialogVisible: false,
                selectedFavoriteCategory: null,
                favoriteCategories: [],
                addToFavoriteCommand: {
                    userId: this.$store.state.user.id
                },
                transferCommand: {
                    fromUserId: this.$store.state.user.id
                },
                favoriteForm: {},
                favoriteRules: {
                    category: [{validator: this.validateCategory, trigger: "change"}]
                },
                transferDialogVisible: false,
                transferForm: {
                    users: []
                },
                transferRules: {
                    selectedUser: [
                        {required: true, message: "请选择转交用户", trigger: "change"}
                    ]
                },
                transfering: false,
                selectionItems: [],
                removingFavorite:false,
                isTransferAll:false
            };
        },

        async mounted() {
            this.search();
        },
        methods: {
            handleSelectionChange(val) {
                this.selectionItems = val;
            },
            handleCategoryChange(id) {
                if(id == "all") {
                    this.consultingList = _.cloneDeep(this.backup.favorites);
                }else {
                    const filtedConsultingList = this.backup.favorites.filter(x=>x.favoriteCategoryId == id);
                    this.consultingList = _.cloneDeep(filtedConsultingList);
                }
            },
            async search() {
                this.loading = true;
                this.$axios.get(api.favoriteConsulting).then(response => {
                    this.backup = _.cloneDeep(response.data);
                    this.favoriteCategories = response.data.categories;
                    this.consultingList = response.data.favorites;
                }).catch(err => {
                    console.log(err.message);
                });
                this.$axios.get(api.usersToBeTransfer).then(response => {
                    this.transferForm.users = response.data;
                }).catch(err => {
                    console.log(err.message);
                });
                this.loading = false;
            },
            removeFavorite() {
                if (this.selectionItems.length == 0) {
                    this.$message({
                        message: "请至少选择一条记录再取消收藏",
                        type: "error"
                    });
                    return;
                }
                this.removingFavorite = true;
                this.$axios.post(api.removeMyFavorite, {ConsultingIds:this.selectionItems.map(x=>x.id)}).then(response => {
                    this.$message({
                        message: "取消收藏成功",
                        type: "success"
                    });
                    this.removingFavorite = false;
                    this.$refs.table.clearSelection();
                    this.search();
                }).catch(err => {
                    this.$message({
                        message: "取消收藏失败: " + err.message,
                        type: "error"
                    });
                    this.removingFavorite = false;
                    this.$refs.table.clearSelection();
                });
            },
            contract(row) {
                this.$router.push({
                    name: "contract",
                    params: {id: row.id}
                });
            },

            edit(id) {
                this.$router.push({
                    name: "consulting-edit",
                    params: {id}
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
                        var index = _.findIndex(this.consultingList, x => x.id == id);
                        this.consultingList.splice(index, 1);
                    });
                } catch (error) {
                    console.log(error);
                }
            },
            details(id) {
                this.$router.push({
                    name: "consulting-details",
                    params: {id}
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
                            var matched = _.find(this.resource.items, x => x.id == this.transferCommand.consultingId);
                            matched.consultingStatusText = "新转交";
                            this.isTransferAll = false;
                            this.transferDialogVisible = false;
                            this.transfering = false;
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
        }
    };
</script>
