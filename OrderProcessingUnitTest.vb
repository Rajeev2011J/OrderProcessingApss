Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports OrderProcessingApplication

Namespace OrderProcessingUnitTest
    <TestClass>
    Public Class OrderProcessingUnitTest
        <TestMethod>
        Public Sub ShouldReturnVideoSlipOnly()
            Dim result = OrderProcessor.ConvertInputToType(New String() {"video", "Random"})
            Assert.AreEqual("Random", result.ItemName)
            Assert.AreEqual("Generated a packing slip.", result.Operations(0))
            Assert.AreEqual(1, result.Operations.Count)
        End Sub

        <TestMethod>
        Public Sub ShouldReturnVideoLearningToSkiSlipOnly()
            Dim result = OrderProcessor.ConvertInputToType(New String() {"video", "Learning To Ski"})
            Assert.AreEqual("Learning To Ski", result.ItemName)
            Assert.AreEqual("Generated a packing slip.", result.Operations(0))
            Assert.AreEqual("'First Aid' video added to the packing slip", result.Operations(1))
            Assert.AreEqual(2, result.Operations.Count)
        End Sub

        <TestMethod>
        Public Sub ShouldReturnUpgradeSlipOnly()
            Dim result = OrderProcessor.ConvertInputToType(New String() {"Upgrade", "Random"})
            Assert.IsNull(result.ItemName)
            Assert.AreEqual("Generated a packing slip.", result.Operations(0))
            Assert.AreEqual("Apply the upgrade", result.Operations(1))
            Assert.AreEqual("Mail Sent", result.Operations(2))
            Assert.AreEqual(3, result.Operations.Count)
        End Sub

        <TestMethod>
        Public Sub ShouldReturnMembershipSlip()
            Dim result = OrderProcessor.ConvertInputToType(New String() {"Membership", "Random"})
            Assert.IsNull(result.ItemName)
            Assert.AreEqual("Generated a packing slip.", result.Operations(0))
            Assert.AreEqual("Activate that membership", result.Operations(1))
            Assert.AreEqual("Mail Sent", result.Operations(2))
            Assert.AreEqual(3, result.Operations.Count)
        End Sub

        <TestMethod>
        Public Sub ShouldReturnBookSlipOnly()
            Dim result = OrderProcessor.ConvertInputToType(New String() {"Book", "Random"})
            Assert.AreEqual("Random", result.ItemName)
            Assert.AreEqual("Generated a packing slip for shipping.", result.Operations(0))
            Assert.AreEqual("Commission payment to the agent", result.Operations(1))
            Assert.AreEqual("Create a duplicate packing slip for the royalty department.", result.Operations(2))
            Assert.AreEqual(3, result.Operations.Count)
        End Sub

        <TestMethod>
        Public Sub ShouldReturnOther()
            Dim result = OrderProcessor.ConvertInputToType(New String() {"other", "Random"})
            Assert.AreEqual("Random", result.ItemName)
            Assert.AreEqual("Generated a packing slip for shipping.", result.Operations(0))
            Assert.AreEqual("Commission payment to the agent", result.Operations(1))
            Assert.AreEqual(2, result.Operations.Count)
            result = OrderProcessor.ConvertInputToType(New String() {"random", "Random"})
            Assert.AreEqual("Random", result.ItemName)
            Assert.AreEqual("Generated a packing slip for shipping.", result.Operations(0))
            Assert.AreEqual("Commission payment to the agent", result.Operations(1))
            Assert.AreEqual(2, result.Operations.Count)
        End Sub
    End Class
End Namespace
