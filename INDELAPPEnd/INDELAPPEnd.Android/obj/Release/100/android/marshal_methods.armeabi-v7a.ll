; ModuleID = 'obj\Release\100\android\marshal_methods.armeabi-v7a.ll'
source_filename = "obj\Release\100\android\marshal_methods.armeabi-v7a.ll"
target datalayout = "e-m:e-p:32:32-Fi8-i64:64-v128:64:128-a:0:32-n32-S64"
target triple = "armv7-unknown-linux-android"


%struct.MonoImage = type opaque

%struct.MonoClass = type opaque

%struct.MarshalMethodsManagedClass = type {
	i32,; uint32_t token
	%struct.MonoClass*; MonoClass* klass
}

%struct.MarshalMethodName = type {
	i64,; uint64_t id
	i8*; char* name
}

%class._JNIEnv = type opaque

%class._jobject = type {
	i8; uint8_t b
}

%class._jclass = type {
	i8; uint8_t b
}

%class._jstring = type {
	i8; uint8_t b
}

%class._jthrowable = type {
	i8; uint8_t b
}

%class._jarray = type {
	i8; uint8_t b
}

%class._jobjectArray = type {
	i8; uint8_t b
}

%class._jbooleanArray = type {
	i8; uint8_t b
}

%class._jbyteArray = type {
	i8; uint8_t b
}

%class._jcharArray = type {
	i8; uint8_t b
}

%class._jshortArray = type {
	i8; uint8_t b
}

%class._jintArray = type {
	i8; uint8_t b
}

%class._jlongArray = type {
	i8; uint8_t b
}

%class._jfloatArray = type {
	i8; uint8_t b
}

%class._jdoubleArray = type {
	i8; uint8_t b
}

; assembly_image_cache
@assembly_image_cache = local_unnamed_addr global [0 x %struct.MonoImage*] zeroinitializer, align 4
; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = local_unnamed_addr constant [108 x i32] [
	i32 34715100, ; 0: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 45
	i32 39109920, ; 1: Newtonsoft.Json.dll => 0x254c520 => 9
	i32 57263871, ; 2: Xamarin.Forms.Core.dll => 0x369c6ff => 37
	i32 177794437, ; 3: Xamarin.Forms.DataGrid => 0xa98ed85 => 38
	i32 182336117, ; 4: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 33
	i32 212497893, ; 5: Xamarin.Forms.Maps.Android => 0xcaa75e5 => 39
	i32 318968648, ; 6: Xamarin.AndroidX.Activity.dll => 0x13031348 => 17
	i32 319314094, ; 7: Xamarin.Forms.Maps => 0x130858ae => 40
	i32 321597661, ; 8: System.Numerics => 0x132b30dd => 14
	i32 342366114, ; 9: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 27
	i32 442521989, ; 10: Xamarin.Essentials => 0x1a605985 => 36
	i32 450948140, ; 11: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 25
	i32 465846621, ; 12: mscorlib => 0x1bc4415d => 8
	i32 469710990, ; 13: System.dll => 0x1bff388e => 13
	i32 627609679, ; 14: Xamarin.AndroidX.CustomView => 0x2568904f => 23
	i32 690569205, ; 15: System.Xml.Linq.dll => 0x29293ff5 => 16
	i32 809851609, ; 16: System.Drawing.Common.dll => 0x30455ad9 => 52
	i32 863990050, ; 17: INDELAPPEnd => 0x337f7122 => 4
	i32 913382283, ; 18: Plugin.Settings => 0x36711b8b => 11
	i32 928116545, ; 19: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 45
	i32 948616524, ; 20: MR.Gestures => 0x388abd4c => 7
	i32 955402788, ; 21: Newtonsoft.Json => 0x38f24a24 => 9
	i32 967690846, ; 22: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 27
	i32 974778368, ; 23: FormsViewGroup.dll => 0x3a19f000 => 3
	i32 1012816738, ; 24: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 32
	i32 1035644815, ; 25: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 19
	i32 1042160112, ; 26: Xamarin.Forms.Platform.dll => 0x3e1e19f0 => 42
	i32 1052210849, ; 27: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 29
	i32 1098259244, ; 28: System => 0x41761b2c => 13
	i32 1134910725, ; 29: Xamarin.Forms.DataGrid.dll => 0x43a55d05 => 38
	i32 1163895655, ; 30: INDELAPPEnd.Android => 0x455fa367 => 0
	i32 1293217323, ; 31: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 24
	i32 1365406463, ; 32: System.ServiceModel.Internals.dll => 0x516272ff => 50
	i32 1376866003, ; 33: Xamarin.AndroidX.SavedState => 0x52114ed3 => 32
	i32 1395363092, ; 34: Plugin.Settings.dll => 0x532b8d14 => 11
	i32 1406073936, ; 35: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 21
	i32 1460219004, ; 36: Xamarin.Forms.Xaml => 0x57092c7c => 43
	i32 1469204771, ; 37: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 18
	i32 1530663695, ; 38: Xamarin.Forms.Maps.dll => 0x5b3c130f => 40
	i32 1565248321, ; 39: Plugin.Settings.Abstractions => 0x5d4bcb41 => 10
	i32 1592978981, ; 40: System.Runtime.Serialization.dll => 0x5ef2ee25 => 2
	i32 1622152042, ; 41: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 30
	i32 1639515021, ; 42: System.Net.Http.dll => 0x61b9038d => 1
	i32 1658251792, ; 43: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 44
	i32 1729485958, ; 44: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 20
	i32 1766324549, ; 45: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 33
	i32 1776026572, ; 46: System.Core.dll => 0x69dc03cc => 12
	i32 1788241197, ; 47: Xamarin.AndroidX.Fragment => 0x6a96652d => 25
	i32 1808609942, ; 48: Xamarin.AndroidX.Loader => 0x6bcd3296 => 30
	i32 1813201214, ; 49: Xamarin.Google.Android.Material => 0x6c13413e => 44
	i32 1867746548, ; 50: Xamarin.Essentials.dll => 0x6f538cf4 => 36
	i32 1878053835, ; 51: Xamarin.Forms.Xaml.dll => 0x6ff0d3cb => 43
	i32 1881862856, ; 52: Xamarin.Forms.Maps.Android.dll => 0x702af2c8 => 39
	i32 1908813208, ; 53: Xamarin.GooglePlayServices.Basement => 0x71c62d98 => 47
	i32 2019465201, ; 54: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 29
	i32 2055257422, ; 55: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 28
	i32 2097448633, ; 56: Xamarin.AndroidX.Legacy.Support.Core.UI => 0x7d0486b9 => 26
	i32 2126786730, ; 57: Xamarin.Forms.Platform.Android => 0x7ec430aa => 41
	i32 2129483829, ; 58: Xamarin.GooglePlayServices.Base.dll => 0x7eed5835 => 46
	i32 2201231467, ; 59: System.Net.Http => 0x8334206b => 1
	i32 2214997456, ; 60: INDELAPPEnd.Android.dll => 0x84062dd0 => 0
	i32 2279755925, ; 61: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 31
	i32 2475788418, ; 62: Java.Interop.dll => 0x93918882 => 5
	i32 2732626843, ; 63: Xamarin.AndroidX.Activity => 0xa2e0939b => 17
	i32 2737747696, ; 64: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 18
	i32 2766581644, ; 65: Xamarin.Forms.Core => 0xa4e6af8c => 37
	i32 2778768386, ; 66: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 34
	i32 2810250172, ; 67: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 21
	i32 2819470561, ; 68: System.Xml.dll => 0xa80db4e1 => 15
	i32 2847418871, ; 69: Xamarin.GooglePlayServices.Base => 0xa9b829f7 => 46
	i32 2853208004, ; 70: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 34
	i32 2905242038, ; 71: mscorlib.dll => 0xad2a79b6 => 8
	i32 2978675010, ; 72: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 24
	i32 3017076677, ; 73: Xamarin.GooglePlayServices.Maps => 0xb3d4efc5 => 48
	i32 3044182254, ; 74: FormsViewGroup => 0xb57288ee => 3
	i32 3058099980, ; 75: Xamarin.GooglePlayServices.Tasks => 0xb646e70c => 49
	i32 3111772706, ; 76: System.Runtime.Serialization => 0xb979e222 => 2
	i32 3204380047, ; 77: System.Data.dll => 0xbefef58f => 51
	i32 3230466174, ; 78: Xamarin.GooglePlayServices.Basement.dll => 0xc08d007e => 47
	i32 3247949154, ; 79: Mono.Security => 0xc197c562 => 53
	i32 3258312781, ; 80: Xamarin.AndroidX.CardView => 0xc235e84d => 20
	i32 3317135071, ; 81: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 23
	i32 3317144872, ; 82: System.Data => 0xc5b79d28 => 51
	i32 3353484488, ; 83: Xamarin.AndroidX.Legacy.Support.Core.UI.dll => 0xc7e21cc8 => 26
	i32 3353544232, ; 84: Xamarin.CommunityToolkit.dll => 0xc7e30628 => 35
	i32 3362522851, ; 85: Xamarin.AndroidX.Core => 0xc86c06e3 => 22
	i32 3366347497, ; 86: Java.Interop => 0xc8a662e9 => 5
	i32 3374999561, ; 87: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 31
	i32 3404865022, ; 88: System.ServiceModel.Internals => 0xcaf21dfe => 50
	i32 3407215217, ; 89: Xamarin.CommunityToolkit => 0xcb15fa71 => 35
	i32 3429136800, ; 90: System.Xml => 0xcc6479a0 => 15
	i32 3476120550, ; 91: Mono.Android => 0xcf3163e6 => 6
	i32 3486231360, ; 92: Plugin.Settings.Abstractions.dll => 0xcfcbab40 => 10
	i32 3509114376, ; 93: System.Xml.Linq => 0xd128d608 => 16
	i32 3536029504, ; 94: Xamarin.Forms.Platform.Android.dll => 0xd2c38740 => 41
	i32 3632359727, ; 95: Xamarin.Forms.Platform => 0xd881692f => 42
	i32 3641597786, ; 96: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 28
	i32 3672681054, ; 97: Mono.Android.dll => 0xdae8aa5e => 6
	i32 3689375977, ; 98: System.Drawing.Common => 0xdbe768e9 => 52
	i32 3736173093, ; 99: MR.Gestures.dll => 0xdeb17a25 => 7
	i32 3829621856, ; 100: System.Numerics.dll => 0xe4436460 => 14
	i32 3896760992, ; 101: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 22
	i32 3955647286, ; 102: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 19
	i32 3970018735, ; 103: Xamarin.GooglePlayServices.Tasks.dll => 0xeca1adaf => 49
	i32 4105002889, ; 104: Mono.Security.dll => 0xf4ad5f89 => 53
	i32 4151237749, ; 105: System.Core => 0xf76edc75 => 12
	i32 4212233174, ; 106: INDELAPPEnd.dll => 0xfb1193d6 => 4
	i32 4278134329 ; 107: Xamarin.GooglePlayServices.Maps.dll => 0xfeff2639 => 48
], align 4
@assembly_image_cache_indices = local_unnamed_addr constant [108 x i32] [
	i32 45, i32 9, i32 37, i32 38, i32 33, i32 39, i32 17, i32 40, ; 0..7
	i32 14, i32 27, i32 36, i32 25, i32 8, i32 13, i32 23, i32 16, ; 8..15
	i32 52, i32 4, i32 11, i32 45, i32 7, i32 9, i32 27, i32 3, ; 16..23
	i32 32, i32 19, i32 42, i32 29, i32 13, i32 38, i32 0, i32 24, ; 24..31
	i32 50, i32 32, i32 11, i32 21, i32 43, i32 18, i32 40, i32 10, ; 32..39
	i32 2, i32 30, i32 1, i32 44, i32 20, i32 33, i32 12, i32 25, ; 40..47
	i32 30, i32 44, i32 36, i32 43, i32 39, i32 47, i32 29, i32 28, ; 48..55
	i32 26, i32 41, i32 46, i32 1, i32 0, i32 31, i32 5, i32 17, ; 56..63
	i32 18, i32 37, i32 34, i32 21, i32 15, i32 46, i32 34, i32 8, ; 64..71
	i32 24, i32 48, i32 3, i32 49, i32 2, i32 51, i32 47, i32 53, ; 72..79
	i32 20, i32 23, i32 51, i32 26, i32 35, i32 22, i32 5, i32 31, ; 80..87
	i32 50, i32 35, i32 15, i32 6, i32 10, i32 16, i32 41, i32 42, ; 88..95
	i32 28, i32 6, i32 52, i32 7, i32 14, i32 22, i32 19, i32 49, ; 96..103
	i32 53, i32 12, i32 4, i32 48 ; 104..107
], align 4

@marshal_methods_number_of_classes = local_unnamed_addr constant i32 0, align 4

; marshal_methods_class_cache
@marshal_methods_class_cache = global [0 x %struct.MarshalMethodsManagedClass] [
], align 4; end of 'marshal_methods_class_cache' array


@get_function_pointer = internal unnamed_addr global void (i32, i32, i32, i8**)* null, align 4

; Function attributes: "frame-pointer"="all" "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+thumb-mode,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" uwtable willreturn writeonly
define void @xamarin_app_init (void (i32, i32, i32, i8**)* %fn) local_unnamed_addr #0
{
	store void (i32, i32, i32, i8**)* %fn, void (i32, i32, i32, i8**)** @get_function_pointer, align 4
	ret void
}

; Names of classes in which marshal methods reside
@mm_class_names = local_unnamed_addr constant [0 x i8*] zeroinitializer, align 4
@__MarshalMethodName_name.0 = internal constant [1 x i8] c"\00", align 1

; mm_method_names
@mm_method_names = local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	; 0
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		i8* getelementptr inbounds ([1 x i8], [1 x i8]* @__MarshalMethodName_name.0, i32 0, i32 0); name
	}
], align 8; end of 'mm_method_names' array


attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable willreturn writeonly "frame-pointer"="all" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+thumb-mode,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }
attributes #1 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable "frame-pointer"="all" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+thumb-mode,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }
attributes #2 = { nounwind }

!llvm.module.flags = !{!0, !1, !2}
!llvm.ident = !{!3}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"min_enum_size", i32 4}
!3 = !{!"Xamarin.Android remotes/origin/d17-5 @ 45b0e144f73b2c8747d8b5ec8cbd3b55beca67f0"}
!llvm.linker.options = !{}
