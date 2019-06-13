// 客户亲属信息
<template>
    <section>
        <!-- <el-row style="border-bottom:1px dashed #ccc">
             <el-col :span="3"><div class="grid-content bg-purple">现金余额</div></el-col>
             <el-col :span="3"><div class="grid-content bg-purple-light">24.00</div></el-col>
             <el-col :span="3"><div class="grid-content bg-purple">待确认金额</div></el-col>
             <el-col :span="3"><div class="grid-content bg-purple">200.00</div></el-col>
             <el-col :span="3"><div class="grid-content bg-purple-light">积分余额</div></el-col>
             <el-col :span="3"><div class="grid-content bg-purple">0</div></el-col>
             <el-col :span="3"><div class="grid-content bg-purple">押金余额</div></el-col>
             <el-col :span="3"><div class="grid-content bg-purple-light">8000.00</div></el-col>
        </el-row>-->
        <el-row>
            <el-col :span="24">
                <div class="grid-content bg-purple-dark">
                    ￥
                    现金信息
                </div>
            </el-col>
            <el-table :data="cash">
                <el-table-column prop="a" label="收支日期"></el-table-column>
                <el-table-column prop="b" label="收支方式"></el-table-column>
                <el-table-column prop="c" label="收入金额"></el-table-column>
                <el-table-column prop="d" label="支出金额"></el-table-column>
                <el-table-column prop="e" label="备注"></el-table-column>
                <el-table-column prop="f" label="收支人"></el-table-column>
                <el-table-column fixed="right" label="操作">
                    <template slot-scope="scope">
                        <el-button type="text" v-if="$hasPermission('删除')">删除</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </el-row>
        <el-row>
            <el-col :span="24">
                <div class="grid-content bg-purple-dark">
                    <i class="el-icon-document"></i>
                    积分信息
                </div>
            </el-col>
            <el-table :data="integral">
                <el-table-column prop="a" label="收支日期"></el-table-column>
                <el-table-column prop="b" label="使用方式"></el-table-column>
                <el-table-column prop="c" label="收入积分"></el-table-column>
                <el-table-column prop="d" label="支出积分"></el-table-column>
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
                    <i class="el-icon-sort"></i>
                    转账信息
                </div>
            </el-col>
            <el-table :data="transferAccounts">
                <el-table-column prop="a" label="收款人姓名"></el-table-column>
                <el-table-column prop="b" label="转账金额"></el-table-column>
                <el-table-column prop="c" label="转账时间"></el-table-column>
                <el-table-column prop="d" label="转账原因"></el-table-column>
                <el-table-column fixed="right" label="操作">
                    <template slot-scope="scope">
                        <el-button type="text">查看</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </el-row>
    </section>
</template>

<script>
export default {
    data() {
        return {
            dialogForm: {},
            dialogFormVisible: false,
            cash: [
                {
                    a: "2018-10-23",
                    b: "现金",
                    c: "5000.00",
                    d: "100.00",
                    e: "预收费用",
                    f: "超级管理员",
                },
                {
                    a: "2018-10-23",
                    b: "现金",
                    c: "5000.00",
                    d: "100.00",
                    e: "预收费用",
                    f: "超级管理员",
                },
                {
                    a: "2018-10-23",
                    b: "现金",
                    c: "5000.00",
                    d: "100.00",
                    e: "预收费用",
                    f: "超级管理员",
                },
                {
                    a: "2018-10-23",
                    b: "现金",
                    c: "5000.00",
                    d: "100.00",
                    e: "预收费用",
                    f: "超级管理员",
                },
                {
                    a: "2018-10-23",
                    b: "现金",
                    c: "5000.00",
                    d: "100.00",
                    e: "预收费用",
                    f: "超级管理员",
                },
            ],
            integral: [],
            transferAccounts: [
                {
                    a: "王护平",
                    b: "200.00",
                    c: "5000.00",
                    d: "test",
                },
            ],
        };
    },



    methods: {
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
.grid-content {
    border-radius: 4px;
    min-height: 36px;
    line-height: 36px;
    font-size: 14px;
    color: #409eff;
}
</style>