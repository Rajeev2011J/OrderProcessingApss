Imports System
Imports System.Collections.Generic
Imports OrderProcessingApplication.Program 

Namespace OrderProcessingApplication
    Public MustInherit Class Product
        Public Property ItemName As String
        Public Property Operations As List(Of String)
        Public MustOverride Sub GetSlip()
    End Class

    Public MustInherit Class PhysicalProduct
        Inherits Product

        Public Overrides Sub GetSlip()
            Operations.Add("Generated a packing slip for shipping.")
            Console.WriteLine("Generated a packing slip for shipping.")
        End Sub

        Public Overridable Sub AddCommission()
            Operations.Add("Commission payment to the agent")
            Console.WriteLine("Commission payment to the agent")
        End Sub
    End Class

    Public MustInherit Class NonPhysicalProduct
        Inherits Product

        Public Overrides Sub GetSlip()
            Operations.Add("Generated a packing slip.")
            Console.WriteLine("Generated a packing slip.")
        End Sub

        Public Overridable Sub DropMail()
            Operations.Add("Mail Sent")
            Console.WriteLine("Mail Sent")
        End Sub
    End Class

    Class Video
        Inherits NonPhysicalProduct

        Public Sub New(ByVal videoName As String)
            Operations = New List(Of String)()
            ItemName = videoName
            GetSlip()
        End Sub

        Public Overrides Sub GetSlip()
            MyBase.GetSlip()

            If ItemName.ToLower().Equals("learning to ski") Then
                Operations.Add("'First Aid' video added to the packing slip")
                Console.WriteLine("'First Aid' video added to the packing slip")
            End If
        End Sub
    End Class

    Class Membership
        Inherits NonPhysicalProduct

        Public Sub New()
            Operations = New List(Of String)()
            MyBase.GetSlip()
            Operations.Add("Activate that membership")
            Console.WriteLine("Activate that membership")
            MyBase.DropMail()
        End Sub
    End Class

    Class Upgrade
        Inherits NonPhysicalProduct

        Public Sub New()
            Operations = New List(Of String)()
            MyBase.GetSlip()
            Operations.Add("Apply the upgrade")
            Console.WriteLine("Apply the upgrade")
            MyBase.DropMail()
        End Sub
    End Class

    Class Book
        Inherits PhysicalProduct

        Public Sub New(ByVal itemName As String)
            ItemName = itemName
            Operations = New List(Of String)()
            MyBase.GetSlip()
            MyBase.AddCommission()
            GetSlip()
        End Sub

        Public Overrides Sub GetSlip()
            Operations.Add("Create a duplicate packing slip for the royalty department.")
            Console.WriteLine("Create a duplicate packing slip for the royalty department.")
        End Sub
    End Class

    Class Other
        Inherits PhysicalProduct

        Public Sub New(ByVal name As String)
            ItemName = name
            Operations = New List(Of String)()
            MyBase.GetSlip()
            MyBase.AddCommission()
        End Sub
    End Class

    Class Program
        Public Enum ProductTypes
            Video
            Membership
            Upgrade
            Book
            Other
        End Enum

        Private Shared Sub Main(ByVal args As String())
            Console.WriteLine("Enter Product type and name (if applicable) seperated by space")
            Dim input = Console.ReadLine().Split(" "c)
            Dim output = OrderProcessor.ConvertInputToType(input)
            Console.WriteLine("Item Name : {0} Operations : {1}", output.ItemName, String.Join(" "c, output.Operations))
            Console.ReadLine()
        End Sub
    End Class

    Public Class OrderProcessor
        Public Shared Function ConvertInputToType(ByVal input As String()) As Product
            Dim type As ProductTypes

            Try
                type = CType([Enum].Parse(GetType(ProductTypes), input(0), ignoreCase:=True), ProductTypes)
            Catch e As Exception
                type = ProductTypes.Other
            End Try

            Dim product As Product
            Dim name As String = If(input.Length > 1, String.Join(" "c, input, 1, input.Length - 1), String.Empty)

            Select Case type
                Case ProductTypes.Membership
                    product = New Membership()
                    Exit Select
                Case ProductTypes.Upgrade
                    product = New Upgrade()
                    Exit Select
                Case ProductTypes.Video
                    product = New Video(name)
                    Exit Select
                Case ProductTypes.Book
                    product = New Book(name)
                    Exit Select
                Case Else
                    product = New Other(name)
                    Exit Select
            End Select

            Return product
        End Function
    End Class
End Namespace
