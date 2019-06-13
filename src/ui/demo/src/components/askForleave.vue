// 客户亲属信息
<template>
    <el-row>
        <el-table :data="tableData">
            <el-table-column prop="a" label="开始时间"></el-table-column>
            <el-table-column prop="b" label="结束时间"></el-table-column>
            <el-table-column prop="c" label="请假原因"></el-table-column>
            <el-table-column prop="d" label="销假时间"></el-table-column>
            <el-table-column prop="e" label="离开天数"></el-table-column>
            <el-table-column fixed="right" label="操作">
                <template slot-scope="scope">
                    <el-button @click="dialogVisible = true" v-if="$hasPermission('编辑')" type="text">编辑</el-button>
                </template>
            </el-table-column>
        </el-table>

        <el-dialog :close-on-click-modal="false" 
             title="销假"
             :visible.sync="dialogVisible"
             width="30%"
             :before-close="handleClose">
                <div class="block">
                    <span class="demonstration">销假时间：</span>
                    <el-date-picker
                      v-model="dateTime"
                      type="datetime"
                      placeholder="选择日期时间">
                    </el-date-picker>
                </div>
                <span class="tips"> 
                    <i class="el-icon-info"></i>
                    销假时间在12点以前，当天不退费
                </span>
                <span slot="footer" class="dialog-footer">
                    <el-button @click="dialogVisible = false">取 消</el-button>
                    <el-button type="primary" @click="dialogVisible = false">确 定</el-button>
                </span>
        </el-dialog>
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
          a: "2018-10-29 11:55",
          b: "2018-10-30 11:55",
          c: "请假回家",
          d: "2018-10-31 15:55",
          e: "3"
        },
        {
          a: "2018-10-29 11:55",
          b: "2018-10-30 11:55",
          c: "请假回家",
          d: "2018-10-31 15:55",
          e: "3"
        },
        {
          a: "2018-10-29 11:55",
          b: "2018-10-30 11:55",
          c: "请假回家",
          d: "2018-10-31 15:55",
          e: "3"
        },
        {
          a: "2018-10-29 11:55",
          b: "2018-10-30 11:55",
          c: "请假回家",
          d: "2018-10-31 15:55",
          e: "3"
        },
       {
          a: "2018-10-29 11:55",
          b: "2018-10-30 11:55",
          c: "请假回家",
          d: "2018-10-31 15:55",
          e: "3"
        }
      ]
    };
  },
  methods: {
    handleClose(done) {
       this.dialogVisible = false
      }
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