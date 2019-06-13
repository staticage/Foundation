// 客户亲属信息
<template>
    <el-row>
        <el-table :data="tableData">
            <el-table-column prop="a" label="序号"></el-table-column>
            <el-table-column prop="b" label="日期"></el-table-column>
            <el-table-column prop="c" label="时间"></el-table-column>
            <el-table-column prop="d" label="项目"></el-table-column>
            <el-table-column prop="e" label="状态"></el-table-column>
            <el-table-column prop="f" label="说明"></el-table-column>
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
          a: "1",
          b: "2018-10-31",
          c: "15:55",
          d: "亦庄",
          e: "在住",
          f: "销假返回",
        },
         {
          a: "2",
          b: "2018-10-31",
          c: "15:55",
          d: "亦庄",
          e: "在住",
          f: "销假返回",
        },
        {
          a: "3",
          b: "2018-10-31",
          c: "15:55",
          d: "亦庄",
          e: "在住",
          f: "销假返回",
        },
         {
          a: "4",
          b: "2018-10-31",
          c: "15:55",
          d: "亦庄",
          e: "在住",
          f: "销假返回",
        },
        {
          a: "5",
          b: "2018-10-31",
          c: "15:55",
          d: "亦庄",
          e: "在住",
          f: "销假返回",
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