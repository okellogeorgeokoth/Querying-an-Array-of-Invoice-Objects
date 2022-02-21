'Name
'Details
Imports System.Text.RegularExpressions
Module Module1

    Sub Main()
        ' Creating an array of invoices
        Dim invoices As Invoice() = {
        New Invoice("83", "Electric Sander", "7", 59.98),
        New Invoice("24", "Power Saw", "18", 99.99),
        New Invoice("7", "Sledge Hammer", "11", 21.5),
        New Invoice("77", "Hammer", "76", 11.99),
        New Invoice("39", "Lawn Mower", "3", 79.5),
        New Invoice("68", "Screwdriver", "106", 6.99),
        New Invoice("56", "Jig Saw", "21", 11.0),
        New Invoice("3", "Wrench", "34", 7.5)}

        'Displaying invoices in an Original Array

        'Part a    Order by PartDescription
        Dim Desc = From i In invoices Order By i.PartDescription Ascending
        Display(Desc, "By Part Description Ascending")

        'Part b    Order by Price
        Dim invPrice = From i In invoices Order By i.Price Ascending
        Display(invPrice, "By Price Ascending")

        'Part c    just Description & quantity, sorted by quantity
        Dim SortDescQuant = From invoice In invoices Select invoice.PartDescription, q = invoice.Quantity Order By q Ascending
        Display2(SortDescQuant, "Part Description and Quantity, sort by Quantity")

        'Part d   add InvoiceTotal and multiply quantity & price
        Dim InvoiceTotal = From i In invoices Select i.PartDescription, t = (i.Quantity * i.Price).ToString("c2") Order By CDec(t) Ascending
        Display2(InvoiceTotal, "Invoice Total")

        'Part e    select InvoiceTotal between $200 & $500
        Dim between = From l In InvoiceTotal Where CDec(l.t) >= 200 AndAlso CDec(l.t) <= 500 Select l
        Display2(between, "Total Prices Between $200 & $500")

        'Display console window for 30 seconds
        Threading.Thread.Sleep(30000)

    End Sub

    Sub Display(Of T)(ByVal results As IEnumerable(Of T), ByVal header As String)
        Console.WriteLine("{0}:", header)

        For Each element As T In results
            Console.WriteLine(" {0}", element)
        Next
        Console.WriteLine()
    End Sub

    Sub Display2(Of T)(ByVal results As IEnumerable(Of T), ByVal header As String)
        Console.WriteLine("{0}:", header)

        For Each element As T In results
            Dim parts(1) As String
            Dim rx As New Regex("(?<=\=\s)(.|\s|,)+?(?=(\s\}|,\s))")
            parts(0) = rx.Matches(element.ToString).Item(0).Value
            parts(1) = rx.Matches(element.ToString).Item(1).Value
            Console.WriteLine(String.Format("{0,-20} {1,9}", parts(0), parts(1)))
        Next
        Console.WriteLine()
    End Sub

End Module
' Creating an Invoice class.
Public Class Invoice
    ' Declaring class variables for Invoice object
    Private partNumberValue As Integer
    Private partDescriptionValue As String
    Private quantityValue As Integer
    Private priceValue As Decimal

    ' Four constructorargument 
    Public Sub New(ByVal part As Integer, ByVal description As String,
       ByVal count As Integer, ByVal pricePerItem As Decimal)

        PartNumber = part
        PartDescription = description
        Quantity = count
        Price = pricePerItem
    End Sub ' New

    ' Property for partNumberValue; no validation necessary
    Public Property PartNumber() As Integer
        Get
            Return partNumberValue
        End Get

        Set(ByVal value As Integer)
            partNumberValue = value
        End Set
    End Property ' PartNumber

    ' Property for partDescriptionValue; no validation necessary
    Public Property PartDescription() As String
        Get
            Return partDescriptionValue
        End Get

        Set(ByVal value As String)
            partDescriptionValue = value
        End Set
    End Property ' PartDescription

    ' Property for quantityValue; ensures value is positive
    Public Property Quantity() As Integer
        Get
            Return quantityValue
        End Get

        Set(ByVal value As Integer)
            ' Checking  whether quantity is positive
            If value > 0 Then
                ' Valid quantity assigned
                quantityValue = value
            End If
        End Set
    End Property ' Quantity

    ' Property for pricePerItemValue; ensures value is positive
    Public Property Price() As Decimal
        Get
            Return priceValue
        End Get

        Set(ByVal value As Decimal)
            ' Determine whether price is positive
            If value > 0 Then
                ' Valid price assigned
                priceValue = value
            End If
        End Set
    End Property ' Price

    ' Return String containing the fields in the Invoice in a clear format
    Public Overrides Function ToString() As String
        ' Left justify each field, and provide large enough spaces so
        ' Line up all the columns 
        Return String.Format("{0,-5} {1,-20} {2,-5} {3,6:C}",
           PartNumber, PartDescription, Quantity, Price)
    End Function ' ToString
End Class ' Invoice
