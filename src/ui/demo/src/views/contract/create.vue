<template>
    <section style="margin-top:-10px;">
        <el-form ref="form" :model="command" :rules="rules" label-width="185px">
            <el-collapse v-model="activeNames">
                <el-collapse-item title="合同基本信息" name="1">
                    <el-row>
                        <el-col :span="8">
                            <el-form-item label="客户编号：">{{command.code}}</el-form-item>
                        </el-col>
                        <!-- 合同编号：编辑时可查看即可 -->
                        <el-col :span="8">
                            <el-form-item label="合同编号：">{{command.contractNo}}</el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="签约销售：">{{command.sellerName}}</el-form-item>
                        </el-col>
                    </el-row>
                    <el-row>
                        <el-col :span="8">
                            <el-form-item label="签署类型：" prop="signedType">
                                <el-select v-model="command.signedType" placeholder="请选择签署类型" style="width:100%">
                                    <el-option label="新签" :value="1"></el-option>
                                    <el-option label="续签" :value="2"></el-option>
                                </el-select>
                            </el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="合同开始日期：" prop="startTime">
                                <el-date-picker v-model="command.startTime" type="date" placeholder="选择日期"
                                                style="width:100%"></el-date-picker>
                            </el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="合同截至日期：" prop="endTime">
                                <el-date-picker v-model="command.endTime" type="date" placeholder="选择日期"
                                                style="width:100%"></el-date-picker>
                            </el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="签约日期：" prop="signedOn">
                                <el-date-picker v-model="command.signedOn" type="date" placeholder="选择日期"
                                                style="width:100%"></el-date-picker>
                            </el-form-item>
                        </el-col>
                    </el-row>
                </el-collapse-item>
                <el-collapse-item title="甲方信息（养老服务机构）" name="2">
                    <el-row>
                        <el-col :span="8">
                            <el-form-item label="公寓名称：" prop="apartmentId">
                                <el-select v-model="command.apartmentId" style="width:100%;" placeholder="请选择公寓名称"
                                           @change="changeApartment">
                                    <el-option v-for="item in apartments" :key="item.id" :label="item.name"
                                               :value="item.id"></el-option>
                                </el-select>
                            </el-form-item>
                        </el-col>
                    </el-row>
                    <el-row>
                        <!-- <el-col :span="8">
                            <el-form-item label="甲方：">{{apartment.name}}</el-form-item>
                        </el-col>-->
                        <el-col :span="8">
                            <el-form-item label="法定代表人：">{{apartment.corporation}}</el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="联系电话：">{{apartment.phone}}</el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="电子邮箱：">{{apartment.email}}</el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="银行户名：">{{apartment.accountName}}</el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="开户行：">{{apartment.bankName}}</el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="银行账号：">{{apartment.accountNumber}}</el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="邮政编码：">{{apartment.postcode}}</el-form-item>
                        </el-col>
                        <!-- <el-col :span="8">
                            <el-form-item label="地址：">{{command.address}}</el-form-item>
                        </el-col>-->
                    </el-row>
                </el-collapse-item>
                <el-collapse-item title="乙方（入住老年人）" name="3">
                    <el-col :span="8">
                        <el-form-item label="乙方姓名：" prop="bName">
                            <el-input v-model="command.bName" placeholder="请输入乙方姓名"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="性别：" prop="bSex">
                            <el-radio-group v-model="command.bSex">
                                <el-radio :label="1">男</el-radio>
                                <el-radio :label="2">女</el-radio>
                            </el-radio-group>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="年龄：" prop="bAge">
                            <el-input-number v-model="command.bAge" controls-position="right" :min="1" :max="120"
                                             :precision="0" label="请输入年龄" style="width:100%;"></el-input-number>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="证件类型：" prop="bCredentialType">
                            <el-radio-group v-model="command.bCredentialType">
                                <el-radio :label="1">居民身份证</el-radio>
                                <el-radio :label="2">护照</el-radio>
                            </el-radio-group>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="证件号：" prop="bIdcard">
                            <el-input v-model="command.bIdcard" placeholder="请输入身份证号"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="联系电话：" prop="bPhone">
                            <el-input v-model="command.bPhone" placeholder="请输入联系电话"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="居住区域：" prop="bArea">
                            <AreaSelector v-model="command.bArea" style="width:100%;"></AreaSelector>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="联系地址：" prop="bAddress">
                            <el-input v-model="command.bAddress" placeholder="请输入联系地址"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="电子邮箱：">
                            <el-input v-model="command.bEmail" placeholder="请输入电子邮箱"></el-input>
                        </el-form-item>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="乙方监护人" name="4">
                    <el-col :span="8">
                        <el-form-item label="乙方监护人姓名：">
                            <el-input v-model="command.cName" placeholder="请输入乙方监护人姓名"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="性别：" prop="sex">
                            <el-radio-group v-model="command.cSex">
                                <el-radio :label="1">男</el-radio>
                                <el-radio :label="2">女</el-radio>
                            </el-radio-group>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="年龄：">
                            <el-input-number v-model="command.cAge" controls-position="right" :min="1" :max="120"
                                             :precision="0" label="请输入年龄" style="width:100%;"></el-input-number>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="与乙方关系：">
                            <ConfigurationSelector v-model="command.cAsTypes" type="销售项配置" name="与客户关系"
                                                   style="width:100%;" placeholder="请选择与乙方关系"></ConfigurationSelector>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="证件类型：" prop="cCredentialType">
                            <el-radio-group v-model="command.cCredentialType">
                                <el-radio :label="1">居民身份证</el-radio>
                                <el-radio :label="2">护照</el-radio>
                            </el-radio-group>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="证件号：">
                            <el-input v-model="command.cIDcard" placeholder="请输入身份证号"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="手机号码：">
                            <el-input v-model="command.cPhone" placeholder="请输入手机号码"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="固定电话：">
                            <el-input v-model="command.cTel" placeholder="请输入固定电话"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="工作单位：">
                            <el-input v-model="command.cCompany" placeholder="请输入工作单位"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="电子邮箱：">
                            <el-input v-model="command.cEmail" placeholder="请输入电子邮箱"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="通讯地址区域：">
                            <AreaSelector v-model="command.cArea" style="width:100%;"></AreaSelector>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="通讯地址：">
                            <el-input v-model="command.cAddress" placeholder="请输入联系地址"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="邮政编码：">
                            <el-input v-model="command.cPostcode" placeholder="请输入邮政编码"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="经常居住区域：">
                            <AreaSelector v-model="command.cLiveArea" style="width:100%;"></AreaSelector>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="经常居住地地址：">
                            <el-input v-model="command.cLiveAddress" placeholder="请输入联系地址"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="乙方监护人（非自然人）名称：">
                            <el-input v-model="command.cLegalPersonCompany" placeholder="请输入乙方监护人（非自然人）名称"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="法人代表：">
                            <el-input v-model="command.cLegalPersonName" placeholder="请输入法人代表"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="电子邮箱：">
                            <el-input v-model="command.cLegalPersonEmail" placeholder="请输入电子邮箱"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="联系人：">
                            <el-input v-model="command.cLegalPersonContactName" placeholder="请输入联系人"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="手机号码：">
                            <el-input v-model="command.cLegalPersonPhone" placeholder="请输入手机号码"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="固定电话：">
                            <el-input v-model="command.cLegalPersonTel" placeholder="请输入固定电话"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="公司区域：">
                            <AreaSelector v-model="command.cLegalPersonArea" style="width:100%;"></AreaSelector>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="公司地址：">
                            <el-input v-model="command.cLegalPersonAddress" placeholder="请输入公司地址"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="邮政编码：">
                            <el-input v-model="command.cLegalPersonPostcode" placeholder="请输入邮政编码"></el-input>
                        </el-form-item>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="丙方" name="5">
                    <el-row>
                        <el-col :span="8">
                            <el-form-item label="丙方作为入住老人的：">
                                <el-select v-model="command.dRelationship" placeholder="请选择" size="mini"
                                           style="width:100%" multiple>
                                    <el-option label="付款义务人" value="0"></el-option>
                                    <el-option label="连带责任保证人" value="1"></el-option>
                                    <el-option label="联系人" value="2"></el-option>
                                    <el-option label="代理人" value="3"></el-option>
                                    <el-option label="其他" value="4"></el-option>
                                </el-select>
                            </el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="丙方姓名：">
                                <el-input v-model="command.dName" placeholder="请输入丙方姓名"></el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="性别：" prop="sex">
                                <el-radio-group v-model="command.dSex">
                                    <el-radio :label="1">男</el-radio>
                                    <el-radio :label="2">女</el-radio>
                                </el-radio-group>
                            </el-form-item>
                        </el-col>
                    </el-row>
                    <el-row>
                        <el-col :span="8">
                            <el-form-item label="与乙方关系：">
                                <ConfigurationSelector v-model="command.dAsTypes" type="销售项配置" name="与客户关系"
                                                       style="width:100%;"
                                                       placeholder="请选择与乙方关系"></ConfigurationSelector>
                            </el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="证件类型：" prop="dCredentialType">
                                <el-radio-group v-model="command.dCredentialType">
                                    <el-radio :label="1">居民身份证</el-radio>
                                    <el-radio :label="2">护照</el-radio>
                                </el-radio-group>
                            </el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="证件号：">
                                <el-input v-model="command.dIDcard" placeholder="请输入身份证号"></el-input>
                            </el-form-item>
                        </el-col>
                    </el-row>
                    <el-row>
                        <el-col :span="8">
                            <el-form-item label="手机号码：">
                                <el-input v-model="command.dPhone" placeholder="请输入手机号码"></el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="固定电话：">
                                <el-input v-model="command.dTel" placeholder="请输入固定电话"></el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="工作单位：">
                                <el-input v-model="command.dCompany" placeholder="请输入工作单位"></el-input>
                            </el-form-item>
                        </el-col>
                    </el-row>
                    <el-row>
                        <el-col :span="8">
                            <el-form-item label="电子邮箱：">
                                <el-input v-model="command.dEmail" placeholder="请输入电子邮箱"></el-input>
                            </el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="通讯区域：">
                                <AreaSelector v-model="command.dArea" style="width:100%;"></AreaSelector>
                            </el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="通讯地址：">
                                <el-input v-model="command.dAddress" placeholder="请输入通讯地址"></el-input>
                            </el-form-item>
                        </el-col>
                    </el-row>
                    <el-col :span="8">
                        <el-form-item label="邮政编码：">
                            <el-input v-model="command.dPostcode" placeholder="请输入邮政编码"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="经常居住地区域：">
                            <AreaSelector v-model="command.dLiveArea" style="width:100%;"></AreaSelector>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="经常居住地地址：">
                            <el-input v-model="command.dLiveAddress" placeholder="请输入经常居住地地址"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="丙方（非自然人）名称：">
                            <el-input v-model="command.dLegalPersonCompany" placeholder="请输入丙方（非自然人）名称"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="法人代表：">
                            <el-input v-model="command.dLegalPersonName" placeholder="请输入法人代表"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="电子邮箱：">
                            <el-input v-model="command.dLegalPersonEmail" placeholder="请输入电子邮箱"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="联系人：">
                            <el-input v-model="command.dLegalPersonContactName" placeholder="请输入法人代表"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="手机号码：">
                            <el-input v-model="command.dLegalPersonPhone" placeholder="请输入手机号码"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="固定电话：">
                            <el-input v-model="command.dLegalPersonTel" placeholder="请输入固定电话"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="公司地址区域：">
                            <AreaSelector v-model="command.dLegalPersonArea" style="width:100%;"></AreaSelector>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="公司地址：">
                            <el-input v-model="command.dLegalPersonAddress" placeholder="请输入公司地址"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <label></label>
                        <span></span>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="邮政编码：">
                            <el-input v-model="command.dLegalPersonPostcode" placeholder="请输入邮政编码"></el-input>
                        </el-form-item>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="房间信息" name="6">
                    <el-col :span="8">
                        <el-form-item label="是否包房：" prop="isCompartment">
                            <el-radio-group v-model="command.isCompartment" @change="changeIsCompartment">
                                <el-radio :label="false">拼房</el-radio>
                                <el-radio :label="true">包房</el-radio>
                            </el-radio-group>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="床位：" prop="buildings">
                            <el-cascader :options="apartment.buildings" v-model="command.buildings" style="width:100%;"
                                         @change="changeBuilding"></el-cascader>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="房型：">{{command.roomType}}</el-form-item>
                    </el-col>
                    <el-col :span="24">
                        <el-form-item label="服务地点：">{{command.apartmentAddress}}</el-form-item>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="费用包" name="7">
                    <el-col :span="8">
                        <el-form-item label="费用包：" prop="pricePackageId">
                            <el-select style="width:100%;" placeholder="请选择费用包" v-model="command.pricePackageId"
                                       @change="changePricePackage">
                                <el-option v-for="item in selectOption.pricePackages" :key="item.id" :label="item.name"
                                           :value="item.id"></el-option>
                            </el-select>
                        </el-form-item>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="服务项目" v-if="serviceVisible" name="8">
                    <el-row>
                        <el-col :span="8">
                            <el-form-item label="生活能力类型：" prop="liveTypeId">
                                <el-select style="width:100%;" placeholder="请选择生活能力类型" v-model="command.liveTypeId"
                                           @change="changeLiveType">
                                    <el-option v-for="item in selectOption.liveTypes" :key="item.id" :label="item.name"
                                               :value="item.id"></el-option>
                                </el-select>
                            </el-form-item>
                        </el-col>
                        <el-col :span="8">
                            <el-form-item label="押金类型：" prop="depositTypeId">
                                <el-select style="width:100%;" placeholder="请选择押金类型" v-model="command.depositTypeId"
                                           @change="changeDepositType">
                                    <el-option v-for="item in selectOption.depositTypes" :key="item.id"
                                               :label="item.name" :value="item.id"></el-option>
                                </el-select>
                            </el-form-item>
                        </el-col>
                    </el-row>
                </el-collapse-item>
                <el-collapse-item title="收费信息" v-if="serviceVisible" name="9">
                    <el-col :span="8">
                        <el-form-item label="押金：">{{command.depositCost}}</el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="一次性安置费：">{{prices.relocationCost}}</el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="床位费：">{{command.roomCost}}</el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="餐费：">{{prices.mealsCost}}</el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="基础服务费：">{{prices.serviceCost}}</el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <!-- 生活能力类型 -->
                        <el-form-item label="生活能力费用：">{{command.serviceMonthlyCost}}</el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="附加服务费：">{{prices.attachCost}}</el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="违约金：">{{prices.liquidatedDamages}}</el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="请假退餐费：">{{prices.refundCost}}</el-form-item>
                    </el-col>
                </el-collapse-item>
                <el-collapse-item title="老带新信息" name="10">
                    <el-col :span="8">
                        <el-form-item label="推荐人：">
                            <el-select
                                    v-model="command.customerId"
                                    filterable
                                    remote
                                    reserve-keyword
                                    placeholder="请输入推荐人"
                                    :remote-method="remoteCustomers"
                                    @change="changeCustomer">
                                <el-option
                                        v-for="item in command.customers"
                                        :key="item.id"
                                        :label="item.name+'('+item.currnetApartmentName+')'"
                                        :value="item.id">
                                </el-option>
                            </el-select>
                        </el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="推荐人奖励积分：">{{command.recommendPoint}}</el-form-item>
                    </el-col>
                    <el-col :span="8">
                        <el-form-item label="被推荐人奖励积分：">{{command.bonusPoint}}</el-form-item>
                    </el-col>
                </el-collapse-item>
            </el-collapse>
        </el-form>
        <el-row class="button-bottom-row">
            <el-button type="primary" :disabled="saving" @click="save">保存</el-button>
            <el-button type="danger" :disabled="saving" @click="submit">提交</el-button>
            <el-button @click="goBack">取消</el-button>
        </el-row>
    </section>
</template>
<script>
    import ConfigurationSelector from "../../components/ConfigurationSelector.vue";
    import AreaSelector from "../../components/AreaSelector.vue";
    import api from "../../config/api";
    import moment from "moment";

    export default {
        components: {
            ConfigurationSelector,
            AreaSelector
        },
        data() {
            return {
                contractTemporary: {},
                serviceVisible: false,
                disabledType: false,
                prices: {},
                activeNames: ["1", "2", "3", "4", "5", "6", "7", "8", "9"],
                apartmentList: null,
                roomTypeList: null,
                saving: false,
                displayState: {chargeDescription: false},
                apartments: [],
                apartment: {
                    buildings: []
                },
                rooms: undefined,
                consulting: {},
                pricePackages: [],
                selectOption: {
                    pricePackages: [],
                    roomTypes: [],
                    depositTypes: [],
                    liveTypes: []
                },
                command: {
                    dRelationship: [],
                    buildings: [],
                    bSex: "",
                    isCompartment: 1,
                    liveTypeId: "",
                    depositTypeId: "",
                    bedId: "",
                    cRelationship: [],
                    signedOn: new Date(),
                    customers: [],
                },
                rules: {
                    signedType: [
                        {
                            required: true,
                            message: "请选择签署类型",
                            trigger: "change"
                        }
                    ],
                    startTime: [
                        {
                            // type: "date",
                            required: true,
                            message: "请选择合同开始日期",
                            trigger: "change"
                        }
                    ],
                    endTime: [
                        {
                            // type: "date",
                            required: true,
                            message: "请选择合同截至日期",
                            trigger: "change"
                        }
                    ],
                    signedOn: [
                        {
                            // type: "date",
                            required: true,
                            message: "请选择签约日期",
                            trigger: "change"
                        }
                    ],
                    apartmentId: [
                        {
                            required: true,
                            validator: this.validateApartment,
                            trigger: "change"
                        }
                    ],
                    bName: [
                        {
                            required: true,
                            message: "请选择乙方姓名",
                            trigger: "blur"
                        }
                    ],
                    bSex: [
                        {
                            required: true,
                            message: "请选择乙方性别",
                            trigger: "change"
                        }
                    ],
                    bAge: [
                        {
                            required: true,
                            message: "请选择乙方年龄",
                            trigger: "blur"
                        }
                    ],
                    bCredentialType: [
                        {
                            required: true,
                            message: "请选择乙方证件类型",
                            trigger: "change"
                        }
                    ],
                    bIdcard: [
                        {
                            required: true,
                            message: "请输入乙方证件号",
                            trigger: "blur"
                        }
                    ],
                    bPhone: [
                        {
                            required: true,
                            message: "请选择乙方联系电话",
                            trigger: "blur"
                        }
                    ],
                    bArea: [
                        {
                            required: true,
                            message: "请选择乙方居住区域",
                            trigger: "blur"
                        }
                    ],
                    bAddress: [
                        {
                            required: true,
                            message: "请选择乙方联系地址",
                            trigger: "blur"
                        }
                    ],
                    isCompartment: [
                        {
                            required: true,
                            message: "请选择是否包房",
                            trigger: "change"
                        }
                    ],
                    buildings: [
                        {required: true, message: "请选择床位", trigger: "change"}
                    ],
                    pricePackageId: [
                        {
                            required: true,
                            message: "请选择费用包",
                            trigger: "change"
                        }
                    ],
                    liveTypeId: [
                        {
                            required: true,
                            message: "请选择生活能力类型",
                            trigger: "change"
                        }
                    ],
                    depositTypeId: [
                        {
                            required: true,
                            message: "请选择押金类型",
                            trigger: "change"
                        }
                    ]
                }
            };
        },

        async mounted() {
            if (this.$route.params.id != undefined) {
                this.apartments = (await this.$axios.get("apartment/select-list")).data;
                this.pricePackages = (await this.$axios.get(
                    "price-package/select-list"
                )).data;

                this.contractTemporary = (await this.$axios.get(
                    "contract/temporary/" + this.$route.params.id
                )).data;

                if (this.contractTemporary) {
                    let command = JSON.parse(this.contractTemporary.content);
                    command.dRelationship = command.dRelationship
                        ? command.dRelationship
                        : [];
                    command.consultingId = this.contractTemporary.consultingId;
                    this.command = command;
                    // 加载楼栋房型等信息
                    this.changeApartment(this.command.apartmentId, true);
                    if (this.command.pricePackageId) {
                        this.changePricePackage(this.command.pricePackageId);
                        this.serviceVisible = true;
                    }
                    this.dateTimeBug();

                    // console.log(this.command.buildings);
                    // if (!this.command.buildings || this.command.buildings.length == 0) {
                    //     this.command.buildings = [""];
                    // }

                    // this.$refs.form.resetFields(); //移除校验结果并重置字段值
                    // this.$refs.form.clearValidate(); // 重置验证
                } else {
                    this.consulting = (await this.$axios.get(
                        "consulting/" + this.$route.params.id
                    )).data;

                    let consulting = _.clone(this.consulting);
                    this.command.consultingId = consulting.id;
                    this.command.sellerName = consulting.sellerName;
                    this.command.apartmentId = consulting.apartmentId;
                    this.command.isCompartment = consulting.isCompartment;
                    this.command.bName = consulting.name;
                    this.command.bAge = consulting.age;
                    this.command.bSex = consulting.sex;
                    this.command.bArea = consulting.area.split(",");
                    this.command.bAddress = consulting.address;
                    this.command.bPhone = consulting.phone;
                    this.changeApartment(this.command.apartmentId, true);
                }
            }

            this.$axios
                .get(api.apartmentDropdown)
                .then(response => {
                    this.apartmentList = response.data.map(x => {
                        return {
                            text: x.name,
                            value: x.id
                        };
                    });
                })
                .catch(err => {
                    console.log(err.message);
                });
        },

        methods: {
            async remoteCustomers(customerName) {
                if (customerName !== '') {
                    this.command.customers = (await this.$axios.post(api.customer + "/query", {name: customerName})).data.items;
                } else {
                    this.command.customers = [];
                }
                console.log(this.command.customers);
            },
            dateTimeBug() {
                if (this.command.startTime)
                    this.command.startTime = moment(this.command.startTime).toDate();
                if (this.command.signedOn)
                    this.command.signedOn = moment(this.command.signedOn).toDate();
                if (this.command.endTime)
                    this.command.endTime = moment(this.command.endTime).toDate();
            }
            ,
            async getBuilding(apartmentId) {
                const buildings = (await this.$axios.get(
                    "apartment/building/" + apartmentId
                )).data.buildings;
                let newBuildings;
                if (this.command.isCompartment == true) {
                    newBuildings = buildings.map(b => {
                        return {
                            ...b,
                            children: b.units.map(u => {
                                return {
                                    ...u,
                                    children: u.floors.map(f => {
                                        return {
                                            ...f,
                                            children: f.rooms
                                                .filter(r => r.enabled && r.roomStatus != 3)
                                                .filter(r => {
                                                    const beds = r.beds.filter(b => b.enabled && !b.occupy);
                                                    if (beds.length == r.beds.length) {
                                                        return {
                                                            ...r,
                                                            children: beds
                                                        };
                                                    }
                                                })
                                        };
                                    })
                                };
                            })
                        };
                    });
                } else {
                    newBuildings = buildings.map(b => {
                        return {
                            ...b,
                            children: b.units.map(u => {
                                return {
                                    ...u,
                                    children: u.floors.map(f => {
                                        return {
                                            ...f,
                                            children: f.rooms
                                                .filter(r => r.enabled && r.roomStatus != 3)
                                                .map(r => {
                                                    return {
                                                        ...r,
                                                        children: r.beds.filter(b => b.enabled && !b.occupy)
                                                    };
                                                })
                                        };
                                    })
                                };
                            })
                        };
                    });
                }
                this.apartment.buildings = newBuildings;
                this.getRoomPrice();
            }
            ,

            async getRoomPrice() {
                const command = _.clone(this.command);
                if (!command.buildings) {
                    return;
                }
                let floorId = command.buildings[2];
                let roomId = command.buildings[3];
                if (roomId && floorId) {

                    if (this.rooms == undefined) {
                        this.rooms = (await this.$axios.get(
                            "room/select-list/" + floorId
                        )).data;
                    }
                    const room = this.rooms.find(room => room.id === roomId);
                    if (room) {
                        command.roomType = room.roomTypeName;
                        if (command.pricePackageId) {
                            let pricePackage = this.selectOption.pricePackages.find(
                                x => x.id == command.pricePackageId
                            );
                            let roomPrice = pricePackage.priceItems.find(
                                x => x.name == room.roomTypeName && x.type == 0
                            );
                            if (roomPrice) {
                                let room;
                                if (this.apartment.buildings.length > 0) {
                                    this.apartment.buildings.filter(b => {
                                        b.units.filter(u => {
                                            u.floors.filter(f => {
                                                f.rooms
                                                    .filter(r => {
                                                        if (r.id == roomId) {
                                                            room = r;
                                                        }
                                                    })
                                            })
                                        })
                                    });
                                }
                                if (room && this.command.isCompartment) {
                                    command.roomCost = roomPrice.price * room.beds.length;// 床位数
                                } else {
                                    command.roomCost = roomPrice.price;
                                }
                                // command.roomType = room.roomTypeName;

                            }
                        }
                    }
                } else {
                    command.roomType = undefined;
                    command.roomCost = undefined;
                }
                this.command = command;
            }
            ,

            validateApartment(rule, value, callback) {
                if (!this.command.apartmentId) {
                    callback(new Error("请选择公寓名称"));
                } else {
                    callback();
                }
            },
            changeIsCompartment(val) {
                this.getBuilding(this.apartment.id);
                // 重置房间信息
                this.command.buildings = [];
            },
            changeBuilding(vals) {
                this.getRoomPrice();
            },
            changeCustomer(customerId) {
                const customer = this.command.customers.find(
                    customer => customer.id === customerId
                );
                const apartment = this.apartments.find(
                    apartment => apartment.id === customer.currnetApartmentId
                );
                this.command.recommendPoint = apartment.recommendPoint;
                this.command.bonusPoint = apartment.bonusPoint;
            },
            changeApartment(apartmentId, notReset) {
                const apartment = this.apartments.find(
                    apartment => apartment.id === apartmentId
                );
                this.apartment = apartment;
                this.apartment.buildings = [];

                this.command.apartmentAddress = apartment.area + apartment.address;
                this.selectOption.pricePackages = this.pricePackages.filter(
                    x => x.apartmentId == apartmentId
                );

                this.prices.relocationCost = apartment.relocationCost;
                this.prices.refundCost = apartment.refund;
                this.prices.attachCost = apartment.attachedServiceCost;
                this.prices.liquidatedDamages = apartment.penaltyPercent + "%";
                // 改变公寓获取推荐人积分启用下列代码
                // this.command.recommendPoint = apartment.recommendPoint;
                // this.command.bonusPoint = apartment.bonusPoint;
                if (notReset == undefined) {
                    // this.$refs["form"].clearValidate(); // 重置验证
                    // 重置服务包相关选项
                    this.command.pricePackageId = "";
                    this.command.liveTypeId = "";
                    this.command.depositTypeId = "";

                    this.command.depositCost = "";
                    // 重置房间信息
                    this.command.buildings = [];
                }
                this.getBuilding(apartmentId);
            }
            ,

            changePricePackage(pricePackageId) {
                let pricePackage = this.selectOption.pricePackages.find(
                    x => x.id == pricePackageId
                );
                this.selectOption.liveTypes = pricePackage.priceItems.filter(
                    x => x.type == 2
                );
                this.selectOption.depositTypes = pricePackage.priceItems.filter(
                    x => x.type == 3
                );
                if (
                    this.selectOption.liveTypes.length > 0 &&
                    this.selectOption.depositTypes.length > 0
                ) {
                    this.serviceVisible = true;
                    this.prices.mealsCost = pricePackage.priceItems.find(
                        x => x.name == "餐费" && x.type == 1
                    ).price;
                    this.prices.serviceCost = pricePackage.priceItems.find(
                        x => x.name == "服务费" && x.type == 1
                    ).price;
                    this.getRoomPrice();
                } else {
                    this.serviceVisible = false;
                }
            }
            ,

            changeLiveType(liveTypeId) {
                let liveType = this.selectOption.liveTypes.find(x => x.id == liveTypeId);
                this.command.serviceMonthlyCost = liveType.price;
            }
            ,

            changeDepositType(depositTypeId) {
                let depositType = this.selectOption.depositTypes.find(
                    x => x.id == depositTypeId
                );
                this.command.depositCost = depositType.price;
            }
            ,

            save() {
                this.$axios
                    .post("contract/save", {
                        id: this.contractTemporary.id,
                        consultingId: this.consulting.id,
                        content: JSON.stringify(this.command)
                    })
                    .then(response => {
                        this.saving = false;
                        this.$message({
                            message: "保存成功",
                            type: "success"
                        });
                        this.$router.push("/sales");
                    })
                    .catch(err => {
                        this.saving = false;
                        this.$message({
                            message: "保存失败," + err.message,
                            type: "error"
                        });
                    });
            }
            ,

            submit() {
                // this.dateTimeBug();
                this.$refs.form.validate(valid => {
                    if (valid) {
                        // console.log(this.command.startTime);
                        this.saving = true;
                        let command = _.clone(this.command);
                        command.bedId = this.command.buildings[4];
                        command.bArea = this.command.bArea.join();
                        if (this.command.cArea) {
                            command.cArea = this.command.cArea.join();
                        }
                        if (this.command.cLiveArea) {
                            command.cLiveArea = this.command.cLiveArea.join();
                        }
                        if (this.command.cLegalPersonArea) {
                            command.cLegalPersonArea = this.command.cLegalPersonArea.join();
                        }
                        if (this.command.dArea) {
                            command.dArea = this.command.dArea.join();
                        }
                        if (this.command.dLiveArea) {
                            command.dLiveArea = this.command.dLiveArea.join();
                        }
                        if (this.command.dLegalPersonArea) {
                            command.dLegalPersonArea = this.command.dLegalPersonArea.join();
                        }
                        this.$axios
                            .post("contract/submit", {
                                contractTemporaryId: this.contractTemporary.id,
                                contract: command
                            })
                            .then(response => {
                                this.saving = false;
                                this.$message({
                                    message: "提交成功",
                                    type: "success"
                                });
                                this.$router.push("/sales");
                            })
                            .catch(err => {
                                this.saving = false;
                                this.$message({
                                    message: "提交失败: " + err.message,
                                    type: "error"
                                });
                            });
                    } else {
                        return false;
                    }
                });
            }
            ,

            goBack() {
                this.$router.go(-1);
            }
            ,

            apartmentChanged(id) {
                this.$axios
                    .get(api.roomTypeDropdown.replace("{id}", id))
                    .then(response => {
                        this.roomTypeList = response.data.map(x => {
                            return {
                                text: x.name,
                                value: x.id
                            };
                        });
                    })
                    .catch(err => {
                        console.log(err.message);
                    });
            }
        }
    }
    ;
</script>
<style>
</style>
