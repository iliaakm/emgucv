﻿//----------------------------------------------------------------------------
//  Copyright (C) 2004-2013 by EMGU. All rights reserved.       
//----------------------------------------------------------------------------

﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.Util;

namespace Emgu.CV.GPU
{
   /// <summary>
   /// Gpu match template buffer, used by the gpu version of MatchTemplate function.
   /// </summary>
   public class GpuTemplateMatching : UnmanagedObject
   {
      /// <summary>
      /// Create a GpuMatchTemplateBuf
      /// </summary>
      /// <param name="method">Specifies the way the template must be compared with image regions </param>
      public GpuTemplateMatching(int srcType, CvEnum.TM_TYPE method, Size blockSize)
      {
         _ptr = GpuInvoke.gpuMatchTemplateBufCreate(srcType, method, ref blockSize);
      }

      /// <summary>
      ///  This function is similiar to cvCalcBackProjectPatch. It slids through image, compares overlapped patches of size wxh with templ using the specified method and stores the comparison results to result
      /// </summary>
      /// <param name="image">Image where the search is running. It should be 8-bit or 32-bit floating-point</param>
      /// <param name="templ">Searched template; must be not greater than the source image and the same data type as the image</param>
      /// <param name="result">A map of comparison results; single-channel 32-bit floating-point. If image is WxH and templ is wxh then result must be W-w+1xH-h+1.</param>
      /// <param name="stream">Use a Stream to call the function asynchronously (non-blocking) or null to call the function synchronously (blocking).</param>  
      public void Match(IntPtr image, IntPtr templ, IntPtr result, Stream stream)
      {
         GpuInvoke.gpuTemplateMatchingMatch(_ptr, image, templ, result, stream);
      }


      /// <summary>
      /// Release the buffer
      /// </summary>
      protected override void DisposeObject()
      {
         GpuInvoke.gpuMatchTemplateBufRelease(ref _ptr);
      }
   }

   public static partial class GpuInvoke
   {
      [DllImport(CvInvoke.EXTERN_GPU_LIBRARY, CallingConvention = CvInvoke.CvCallingConvention)]
      internal static extern IntPtr gpuMatchTemplateBufCreate(int srcType, CvEnum.TM_TYPE method, ref Size blockSize);

      [DllImport(CvInvoke.EXTERN_GPU_LIBRARY, CallingConvention = CvInvoke.CvCallingConvention)]
      internal static extern void gpuMatchTemplateBufRelease(ref IntPtr buf);

      /// <summary>
      /// This function is similiar to cvCalcBackProjectPatch. It slids through image, compares overlapped patches of size wxh with templ using the specified method and stores the comparison results to result
      /// </summary>
      /// <param name="image">Image where the search is running. It should be 8-bit or 32-bit floating-point</param>
      /// <param name="templ">Searched template; must be not greater than the source image and the same data type as the image</param>
      /// <param name="result">A map of comparison results; single-channel 32-bit floating-point. If image is WxH and templ is wxh then result must be W-w+1xH-h+1.</param>
      /// <param name="tm">Pointer to cv::gpu::TemplateMatching</param>
      /// <param name="stream">Use a Stream to call the function asynchronously (non-blocking) or IntPtr.Zero to call the function synchronously (blocking).</param>  
      [DllImport(CvInvoke.EXTERN_GPU_LIBRARY, CallingConvention = CvInvoke.CvCallingConvention, EntryPoint = "gpuTemplateMatchingMatch")]
      internal static extern void gpuTemplateMatchingMatch(IntPtr tm, IntPtr image, IntPtr templ, IntPtr result, IntPtr stream);


   }
}
