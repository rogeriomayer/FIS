﻿﻿// This file was auto-generated by ML.NET Model Builder. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.FastTree;
using Microsoft.ML.Trainers;
using Microsoft.ML;

namespace FMC_FIS_EnvioEmailCredz1
{
    public partial class MLGeneric
    {
        public static ITransformer RetrainPipeline(MLContext context, IDataView trainData)
        {
            var pipeline = BuildPipeline(context);
            var model = pipeline.Fit(trainData);

            return model;
        }

        /// <summary>
        /// build the pipeline that is used from model builder. Use this function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.Categorical.OneHotEncoding(@"col0", @"col0")      
                                    .Append(mlContext.Transforms.ReplaceMissingValues(new []{new InputOutputColumnPair(@"col1", @"col1"),new InputOutputColumnPair(@"col4", @"col4"),new InputOutputColumnPair(@"col5", @"col5"),new InputOutputColumnPair(@"col6", @"col6"),new InputOutputColumnPair(@"col7", @"col7"),new InputOutputColumnPair(@"col8", @"col8")}))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(@"col2", @"col2"))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(@"col3", @"col3"))      
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new []{@"col0",@"col1",@"col4",@"col5",@"col6",@"col7",@"col8",@"col2",@"col3"}))      
                                    .Append(mlContext.Regression.Trainers.FastForest(new FastForestRegressionTrainer.Options(){NumberOfTrees=313,FeatureFraction=0.732343853657246F,LabelColumnName=@"col9",FeatureColumnName=@"Features"}));

            return pipeline;
        }
    }
}
