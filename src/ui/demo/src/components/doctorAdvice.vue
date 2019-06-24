// 客户亲属信息
<template>
    <el-row>
        <el-table :data="tableData">
            <el-table-column prop="a" label="医嘱类型"></el-table-column>
            <el-table-column prop="b" label="开始日期"></el-table-column>
            <el-table-column prop="c" label="停止日期"></el-table-column>
            <el-table-column prop="d" label="医嘱内容"></el-table-column>
            <el-table-column prop="e" label="是否药品"></el-table-column>
            <el-table-column prop="f" label="填开医生"></el-table-column>
            <el-table-column prop="g" label="备注"></el-table-column>
            <el-table-column prop="h" label="状态"></el-table-column>
            <el-table-column fixed="right" label="操作">
                <template slot-scope="scope">
                    <el-button  type="text">护士确认</el-button>
                    <el-button @click="dialogFormVisible = true" type="text" v-if="$hasPermission('编辑')">编辑</el-button>
                    <el-button  type="text" style="color:red" v-if="$hasPermission('删除')">删除</el-button>
                    <el-button  type="text" style="color:orange">作废</el-button>
                </template>
            </el-table-column>
        </el-table>
         <el-dialog :close-on-click-modal="false"  title="编辑客户长期医嘱信息" :visible.sync="dialogFormVisible">
            <el-form :model="form">
                 <el-row>
                     <el-col :span="12">
                         <div class="block">
                             <span class="demonstration">开始日期：</span>
                             <el-date-picker
                               v-model="dateTime"
                               type="datetime"
                               placeholder="选择日期时间">
                             </el-date-picker>
                        </div>
                     </el-col>
                    <el-col :span="12">
                         <el-form-item label="是否用药" :label-width="formLabelWidth">
                            <el-select v-model="region" placeholder="请选择" @change="choose">
                              <el-option
                                v-for="item in chooseYes"
                                :key="item.value"
                                :label="item.label"
                                :value="item.value">
                             </el-option>
                            </el-select>
                        </el-form-item>
                    </el-col>
                </el-row>
                <div v-if="contentHide">
                    <el-row>
                        <el-col :span="24">
                            <el-form-item label="医嘱内容：">
                                <el-input v-model="form.content" autocomplete="on" style="width:90%"></el-input>
                            </el-form-item>
                        </el-col>
                    </el-row>
                    <el-row>
                        <el-col :span="12">
                            <el-form-item label="单次剂量：">
                                <el-input v-model="form.dosage" autocomplete="on" style="width:57%"></el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :span="12">
                            <el-form-item label="每日频次：" :label-width="formLabelWidth">
                                <el-input v-model="form.times" autocomplete="on"></el-input>
                            </el-form-item>
                        </el-col>
                    </el-row>
                    <el-row>
                        <el-col :span="12">
                            <el-form-item label="给药途径：">
                                <el-select v-model="form.use" placeholder="请选择">
                                    <el-option label="外用" value="shanghai"></el-option>
                                    <el-option label="口服" value="beijing"></el-option>
                                </el-select>
                            </el-form-item>
                        </el-col>
                        <el-col :span="12">
                            <el-form-item label="用药时间：" :label-width="formLabelWidth">
                                <el-input v-model="form.time" autocomplete="on"></el-input>
                            </el-form-item>
                        </el-col>
                    </el-row>
                </div>

            </el-form>
            <div slot="footer" class="dialog-footer">
              <el-button @click="dialogFormVisible = false">取 消</el-button>
              <el-button type="primary" @click="dialogFormVisible = false">确 定</el-button>
            </div>
     </el-dialog>
    </el-row>
    
</template>

<script>
export default {
  data() {
    return {
        dateTime: '',
        contentHide:true,
        dialogFormVisible: false,
        chooseYes: [{
          value: '选项1',
          label: '是'
        }, {
          value: '选项2',
          label: '否'
        }],
        region:'请选择',
        form: {  
          use: '请选择',
          content: '饭前',
          dosage: '3次',
          times:'2',
          time: '13:00',
        },
      formLabelWidth: '120px',
      dialogForm: {},
      tableData: [
        {
          a: "临时医嘱",
          b: "2018-10-30 11:55",
          c: "2018/9/26 23:59:59",
          d: "多喝热水",
          e: "是",
          f:'超级管理员',
          g:'备注。。。',
          h:'生效'
        },
        {
          a: "临时医嘱",
          b: "2018-10-30 11:55",
          c: "2018/9/26 23:59:59",
          d: "多喝热水",
          e: "是",
          f:'超级管理员',
          g:'备注。。。',
          h:'生效'
        },
       {
          a: "临时医嘱",
          b: "2018-10-30 11:55",
          c: "2018/9/26 23:59:59",
          d: "多喝热水",
          e: "是",
          f:'超级管理员',
          g:'备注。。。',
          h:'生效'
        },
        {
          a: "临时医嘱",
          b: "2018-10-30 11:55",
          c: "2018/9/26 23:59:59",
          d: "多喝热水",
          e: "是",
          f:'超级管理员',
          g:'备注。。。',
          h:'生效'
        }
      ]
    };
  },
  methods: {
      choose (){
          if (this.region === '选项1') {
              this.contentHide = true
          }else {
              this.contentHide = false
          }
      }
    }
}
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