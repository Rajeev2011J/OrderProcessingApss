module PaymentSystem 

(*Types*)
type ProductId =          ProductId of string
type MemberId =           MemberId  of string
type Email =              Email     of string
type Agent =              Agent
type RoyaltyDepartment =  RoyaltyDepartment

type PackingSlip = {
    MemberId:MemberId
    ProductId:ProductId
}

type PhysicalProducts =
    | Book
    | Video
    | Other

type MembershipType =
    | Membership of MemberId
    | Upgrade    of MemberId

type PaymentFor =
    | PhysicalProduct of PhysicalProducts * PackingSlip
    | Membership      of MembershipType

type PackingSlipOptions =
    | PackingSlip       of PackingSlip
    | DuplicateSlips    of PackingSlip
    | WithFirstAidVideo of PackingSlip

type PaymentResponse =
    | PackingSlip        of PackingSlipOptions
    | ActivateMembership of MemberId
    | UpgradeMembership  of MemberId
    | EmailOwner         of MembershipType
    | CommissionPayment  of Agent

(*Functions*)

let publish payload = ()       // Stub
let getAgent productId = Agent // Stub

let respondTo (payment:PaymentFor) =

    match payment with
    | PhysicalProduct     (kind , packingSlip) -> 

        publish (CommissionPayment (getAgent packingSlip.ProductId))

        match kind with
        | Book  -> publish (DuplicateSlips    packingSlip)
        | Video -> publish (WithFirstAidVideo packingSlip)
        | Other -> publish packingSlip

    | Membership kind ->

        publish(EmailOwner kind)

        match kind with
        | MembershipType.Membership memberId -> publish(ActivateMembership memberId)
        | MembershipType.Upgrade    memberId -> publish(UpgradeMembership  memberId)    
