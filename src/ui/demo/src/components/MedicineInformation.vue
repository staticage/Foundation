// 客户亲属信息
<template>
    <el-row>
        <el-table :data="tableData">
            <el-table-column prop="a" label="药品名称"></el-table-column>
            <el-table-column prop="b" label="单次剂量"></el-table-column>
            <el-table-column prop="c" label="给药途径"></el-table-column>
            <el-table-column prop="d" label="服药日期"></el-table-column>
            <el-table-column prop="e" label="服药时间"></el-table-column>
            <el-table-column prop="f" label="填开医生"></el-table-column>
            <el-table-column prop="g" label="状态"></el-table-column>
            <el-table-column fixed="right" label="操作">
                <template slot-scope="scope">
                    <el-button @click="dialogVisible = true" type="text">查看</el-button>
                </template>
            </el-table-column>
        </el-table>

    </el-row>
</template>

<script>
export default {
  data() {
    return {
      dateTime: '',
      dialogForm: {},
      dialogVisible: false,
      tableData: [
        {
          a: "山楂片",
          b: "1",
          c: "口服",
          d: "2018-10-31",
          e: "11：00",
          f:"超级管理员",
          g:"已发药"
        },
        {
          a: "山楂片",
          b: "1",
          c: "口服",
          d: "2018-10-31",
          e: "11：00",
          f:"超级管理员",
          g:"已发药"
        },
        {
          a: "山楂片",
          b: "1",
          c: "口服",
          d: "2018-10-31",
          e: "11：00",
          f:"超级管理员",
          g:"已发药"
        }
      ]
    };
  },
  methods: {
   
    }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style lang="scss">
.el-dialog__body {
    padding: 30px 100px;
}
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
.tips {
    color: #409EFF;
    padding-top: 10px;
    display: inline-block;
}

</style>