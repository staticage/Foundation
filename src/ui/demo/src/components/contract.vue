// 客户亲属信息
<template>
    <section class="contractBox">
        <el-row>
            <el-col :span="24">
                <div class="grid-content bg-purple-dark">
                    <i class="el-icon-message"></i>
                    合同信息
                </div>
            </el-col>
            <el-table :data="resource.contracts">
                <el-table-column prop="contractNo" label="合同编号"></el-table-column>
                <el-table-column label="起始日期">
                    <template slot-scope="scope">{{moment(scope.row.startTime).format("YYYY-MM-DD")}}</template>
                </el-table-column>
                <el-table-column label="结束日期">
                    <template slot-scope="scope">{{moment(scope.row.endTime).format("YYYY-MM-DD")}}</template>
                </el-table-column>
                <el-table-column prop="d" label="退住日期"></el-table-column>
                <el-table-column prop="creatorName" label="签约销售"></el-table-column>
                <el-table-column prop="f" label="签约金额"></el-table-column>
                <el-table-column prop="statusText" label="状态"></el-table-column>
                <el-table-column fixed="right" label="操作">
                    <template slot-scope="scope">
                        <el-button @click="details(scope.row)" type="text">查看</el-button>
                        <el-button type="text">打印</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </el-row>
        <el-row>
            <el-col :span="24">
                <div class="grid-content bg-purple-dark">
                    ￥
                    费用变更补充协议
                </div>
            </el-col>
            <el-table :data="RateChange">
                <el-table-column prop="a" label="合同编号"></el-table-column>
                <el-table-column prop="b" label="起始日期"></el-table-column>
                <el-table-column prop="c" label="结束日期"></el-table-column>
                <el-table-column prop="d" label="变更类型"></el-table-column>
                <el-table-column prop="e" label="变更额度"></el-table-column>
                <el-table-column prop="f" label="备注">
                    <template slot-scope="scope" class="tooltipTitle">
                        <el-tooltip class="item" effect="dark" :content="scope.row.f" placement="top-start">
                            <el-button class="tooltipTitle">{{scope.row.f}}</el-button>
                        </el-tooltip>
                    </template>
                    <template></template>
                </el-table-column>
                <el-table-column prop="g" label="状态"></el-table-column>
                <el-table-column fixed="right" label="操作">
                    <template slot-scope="scope">
                        <el-button type="text">查看</el-button>
                        <el-button type="text">打印</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </el-row>
        <el-row>
            <el-col :span="24">
                <div class="grid-content bg-purple-dark">
                    <i class="el-icon-document"></i>
                    服务包补充协议
                </div>
            </el-col>
            <el-table :data="resource.servicePackChanges">
                <el-table-column prop="contractNo" label="合同编号"></el-table-column>
                <el-table-column label="起始日期">
                    <template slot-scope="scope">{{moment(scope.row.changeDate).format("YYYY-MM-DD")}}</template>
                </el-table-column>
                <el-table-column label="结束日期">
                    <template slot-scope="scope">{{moment(scope.row.changeEndDate).format("YYYY-MM-DD")}}</template>
                </el-table-column>
                <el-table-column prop="e" label="生活能力类型"></el-table-column>
                <el-table-column prop="f" label="总费"></el-table-column>
                <el-table-column prop="statusText" label="状态"></el-table-column>
                <el-table-column fixed="right" label="操作">
                    <template slot-scope="scope">
                        <el-button type="text">查看</el-button>
                        <el-button type="text">打印</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </el-row>
        <el-row>
            <el-col :span="24">
                <div class="grid-content bg-purple-dark">
                    <i class="el-icon-refresh"></i>
                    换房补充协议
                </div>
            </el-col>
            <el-table :data="resource.roomChanges">
                <el-table-column prop="contractNo" label="合同编号"></el-table-column>
                <el-table-column label="换房日期">
                    <template slot-scope="scope">{{moment(scope.row.startTime).format("YYYY-MM-DD")}}</template>
                </el-table-column>
                <el-table-column label="结束日期">
                    <template slot-scope="scope">{{moment(scope.row.startTime).format("YYYY-MM-DD")}}</template>
                </el-table-column>
                <el-table-column prop="d" label="房间号"></el-table-column>
                <el-table-column prop="e" label="是否包房"></el-table-column>
                <el-table-column prop="f" label="独立月费"></el-table-column>
                <el-table-column prop="statusText" label="状态"></el-table-column>
                <el-table-column fixed="right" label="操作">
                    <template slot-scope="scope">
                        <el-button type="text">查看</el-button>
                        <el-button type="text">打印</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </el-row>
    </section>
</template>

<script>
import api from "../config/api";

export default {
    props: {
        value: {
            type: String,
            default: function () {
                return "";
            }
        }
    },
    data() {
        return {
            contractId: undefined,
            customerId: undefined,
            dialogForm: {},
            dialogFormVisible: false,
            resource: {},
            RateChange: [
                {
                    a: "CXM-BJYZ-RZ-20151106",
                    b: "2018-10-31",
                    c: "2018-11-31",
                    d: "比例变更",
                    e: "-8.00 %",
                    f: "新价格调整，续签新价格调整，续签...新价格调整，续签新价格调整，续签...",
                    g: "失效"
                },
            ]
        };
    },
    mounted() {

        this.$watch("value", newValue => {
            if (!this.customerId) {
                this.customerId = _.clone(this.value);
                this.init();
            }
        });
        // this.$watch("visible", newValue => {
        //     this.dialogFormVisible = newValue;
        //     if (this.dialogFormVisible) {
        //         if (!this.contract) {
        //             this.init();
        //         }
        //         this.command = {};
        //         if (this.$refs["form"]) {
        //             this.$refs["form"].clearValidate();
        //         }
        //     }
        // });
        // this.$watch("dialogFormVisible", newValue => {
        //     this.dialogFormVisible = newValue;
        //     if (newValue === false) {
        //         this.$emit("callback", {
        //             visible: this.dialogFormVisible,
        //             status: this.status
        //         });
        //         this.reset();
        //     } else {
        //     }
        // });
    },
    methods: {
        details(row) {
            this.$router.push({
                name: "contract-details",
                params: { id: row.id }
            });
        },
        async init() {
            this.resource = (await this.$axios.get(
                api.contract + "/customer/" + this.customerId
            )).data;
            // console.log(this.resource);
        },
        edit() {
            this.dialogFormVisible = true;
        },
    }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style lang="scss">
/*.el-tooltip__popper.is-dark {*/
/*width: 200px;*/
/*}*/
</style>
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
.grid-content {
    border-radius: 4px;
    min-height: 36px;
    line-height: 36px;
    font-size: 14px;
    color: #409eff;
}
.tooltipTitle {
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
    width: 100%;
}
</style>