using libsvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GestureStudio
{
    class SvmProblemBuilder
    {
        double[][] problemSpace;
        double[] labels;

        public SvmProblemBuilder(double[] labels, double[][] problemSpace)
        {
            this.problemSpace = problemSpace;
            this.labels = labels;
        }

        public svm_problem CreateProblem()
        {
            svm_problem problem = new svm_problem();
            problem.l = labels.Length;
            problem.y = labels;
            problem.x = this.problemSpace.Select(problemVector => MatrixUtil.DoubleToSvmNode(problemVector)).ToArray();

            return problem;
        }
    }
}
