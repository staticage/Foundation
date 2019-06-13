// 客户亲属信息
<template>
    <el-row>
        <el-table :data="tableData">
            <el-table-column prop="a" label="账单类型"></el-table-column>
            <el-table-column prop="b" label="账单日期"></el-table-column>
            <el-table-column prop="c" label="合同编号"></el-table-column>
            <el-table-column prop="d" label="账单状态"></el-table-column>
            <el-table-column prop="e" label="账单金额"></el-table-column>
            <el-table-column prop="f" label="已缴金额"></el-table-column>
            <el-table-column prop="g" label="未缴金额"></el-table-column>
            <el-table-column fixed="right" label="操作">
                <template slot-scope="scope">
                    <el-button @click="edit(scope.row)" type="text">查看</el-button>
                </template>
            </el-table-column>
        </el-table>
    </el-row>
</template>

<script>
export default {
  data() {
    return {
      dialogForm: {},
      dialogFormVisible: false,
      tableData: [
        {
          a: "免检账单",
          b: "2018年8月",
          c: "SLL-CB-BJYZ-NO.20171107",
          d: "已结清",
          e: "1.00",
          f: 480,
          g: "0",
        },
         {
          a: "免检账单",
          b: "2018年8月",
          c: "SLL-CB-BJYZ-NO.20171107",
          d: "已结清",
          e: "1.00",
          f: 480,
          g: "0",
        },
        {
          a: "免检账单",
          b: "2018年8月",
          c: "SLL-CB-BJYZ-NO.20171107",
          d: "已结清",
          e: "1.00",
          f: 480,
          g: "0",
        },
         {
          a: "免检账单",
          b: "2018年8月",
          c: "SLL-CB-BJYZ-NO.20171107",
          d: "已结清",
          e: "1.00",
          f: 480,
          g: "0",
        },
        {
          a: "免检账单",
          b: "2018年8月",
          c: "SLL-CB-BJYZ-NO.20171107",
          d: "已结清",
          e: "1.00",
          f: 480,
          g: "0",
        },
      ]
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
</style>