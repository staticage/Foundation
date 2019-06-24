// 客户亲属信息
<template>
    <section>
        <el-row>
            <el-table :data="resource.items">
                <el-table-column label="服务时间">
                    <template slot-scope="scope">{{moment(scope.row.serviceTime).format("YYYY-MM-DD")}}
                    </template>
                </el-table-column>
                <el-table-column prop="serviceName" label="服务项名称"></el-table-column>
                <el-table-column prop="serviceCost" label="服务单价"></el-table-column>
                <el-table-column label="耗材金额">
                    <template slot-scope="scope">
                        {{scope.row.consumableCost>0?"":"无"}}
                        <el-button v-if="scope.row.consumableCost>0" @click="showConsumables(scope.row)" type="text">
                            {{scope.row.consumableCost}}
                        </el-button>
                    </template>
                </el-table-column>
                <el-table-column prop="serviceCount" label="服务次数"></el-table-column>
                <el-table-column prop="total" label="总金额"></el-table-column>
                <el-table-column prop="decription" label="备注"></el-table-column>
                <el-table-column prop="actorName" label="记录人"></el-table-column>
                <!--<el-table-column fixed="right" label="操作">-->
                <!--<template slot-scope="scope">-->
                <!--<el-button @click="edit(scope.row)" type="text" v-if="$hasPermission('编辑')">编辑</el-button>-->
                <!--<el-button type="text" v-if="$hasPermission('删除')">删除</el-button>-->
                <!--</template>-->
                <!--</el-table-column>-->
            </el-table>
        </el-row>
        <el-dialog title="耗材信息" :visible.sync="dialogConsumableVisible" width="40%">
            <el-table :data="consumables">
                <el-table-column prop="name" label="耗材名称"></el-table-column>
                <el-table-column prop="count" label="数量"></el-table-column>
                <el-table-column prop="cost" label="单价/元"></el-table-column>
            </el-table>
        </el-dialog>
    </section>
</template>

<script>
    import api from "../config/api";

    export default {
        props: {
            id: {
                type: String,
                default: function () {
                    return "";
                }
            }
        },
        data() {
            return {
                dialogForm: {},
                dialogFormVisible: false,
                dialogConsumableVisible: false,
                query: {pageSize: 2000},
                resource: {},
                consumables: []
            };
        },

        async mounted() {
            this.$watch("id", newValue => {
                this.init();
            });
        },

        methods: {
            async showConsumables(row) {
                this.dialogConsumableVisible = true;
                this.consumables = (await this.$axios.get(
                    api.valueAddedService + "/consumable/" + row.id
                )).data;
            },
            async search() {
                this.resource = (await this.$axios.post(
                    api.valueAddedService + "/query",
                    this.query
                )).data;
            },
            init() {
                this.query.customerId = this.$props.id;
                this.search();
            },
            edit() {
                this.dialogFormVisible = true;
            },
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