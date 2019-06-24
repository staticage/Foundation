// 客户亲属信息
<template>
    <el-row>
        <el-table :data="tableData">
            <el-table-column prop="a" label="序号"></el-table-column>
            <el-table-column prop="b" label="开始日期"></el-table-column>
            <el-table-column prop="c" label="结束日期"></el-table-column>
            <el-table-column prop="d" label="项目"></el-table-column>
            <el-table-column prop="e" label="楼栋"></el-table-column>
            <el-table-column prop="f" label="单元"></el-table-column>
            <el-table-column prop="g" label="房间"></el-table-column>
            <el-table-column prop="h" label="床位"></el-table-column>
            <el-table-column prop="i" label="生活能力类型"></el-table-column>
            <el-table-column prop="j" label="月费"></el-table-column>
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
          c: "2018-11-31",
          d: "亦庄",
          e: "2栋",
          f: "1单元",
          g: "520",
          h: "包房",
          i: "生活照料4级",
          j: "20772.00",

        },
         {
          a: "2",
          b: "2018-10-31",
          c: "2018-11-31",
          d: "亦庄",
          e: "2栋",
          f: "1单元",
          g: "520",
          h: "包房",
          i: "生活照料4级",
          j: "20772.00",
        },
        {
          a: "3",
          b: "2018-10-31",
          c: "2018-11-31",
          d: "亦庄",
          e: "2栋",
          f: "1单元",
          g: "520",
          h: "包房",
          i: "生活照料4级",
          j: "20772.00",
        },
         {
          a: "4",
          b: "2018-10-31",
          c: "2018-11-31",
          d: "亦庄",
          e: "2栋",
          f: "1单元",
          g: "520",
          h: "包房",
          i: "生活照料4级",
          j: "20772.00",
        },
        {
          a: "5",
          b: "2018-10-31",
          c: "2018-11-31",
          d: "亦庄",
          e: "2栋",
          f: "1单元",
          g: "520",
          h: "包房",
          i: "生活照料4级",
          j: "20772.00",
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