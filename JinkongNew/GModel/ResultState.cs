namespace GModel
{
    public enum ResultState
    {
        SUCCEED,//成功
        VALUENULL,//传入值为Null
        ERROR,//失败
        DEFAULT,//初始值
        PWDERROR,//密码错误
        LNAMEERROR,//登陆名错误
        EXIST,//存在
        NOTEXIST,//
        RELEVANCE,//有关联关系存在
        UNCONNECTING, //当前数据库连接失败
        ISBING,//已绑定
        ISNULL,//传入值为空
        TerminalNumEXIST,//GPS编号已存在
        VINCODEEXIST,//vin编号已存在
        BrandEXIST,//品牌已存在
        SeriesEXIST,//系列已存在
        DeviceTypeEXIST,//设备类型已存在
        AdminUnlock, //管理员锁车
        EmployeeUnlock  //工厂员工锁车
    }
}
