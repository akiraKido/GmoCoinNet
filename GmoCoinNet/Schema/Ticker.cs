namespace GmoCoinNet.Schema;

/// <summary>Available cryptocurrency trading pairs on GMO Coin</summary>
/// <summary xml:lang="ja">GMOコインで取引可能な暗号資産ペア</summary>
public enum Ticker
{
    #region Spot Trading / 現物取引

    /// <summary>Bitcoin</summary>
    Btc,
    /// <summary>Ethereum</summary>
    Eth,
    /// <summary>Bitcoin Cash</summary>
    Bch,
    /// <summary>Litecoin</summary>
    Ltc,
    /// <summary>Ripple</summary>
    Xrp,
    /// <summary>NEM</summary>
    Xem,
    /// <summary>Stellar Lumens</summary>
    Xlm,
    /// <summary>Basic Attention Token</summary>
    Bat,
    /// <summary>Tezos</summary>
    Xtz,
    /// <summary>Qtum</summary>
    Qtum,
    /// <summary>Enjin Coin</summary>
    Enj,
    /// <summary>Polkadot</summary>
    Dot,
    /// <summary>Cosmos</summary>
    Atom,
    /// <summary>Maker</summary>
    Mkr,
    /// <summary>Dai</summary>
    Dai,
    /// <summary>Symbol</summary>
    Xym,
    /// <summary>MonaCoin</summary>
    Mona,
    /// <summary>FCR</summary>
    Fcr,
    /// <summary>Cardano</summary>
    Ada,
    /// <summary>Chainlink</summary>
    Link,
    /// <summary>Dogecoin</summary>
    Doge,
    /// <summary>Solana</summary>
    Sol,
    /// <summary>Astar</summary>
    Astr,
    /// <summary>NOT A HOTEL COIN</summary>
    Nac,
    
    #endregion

    #region Leveraged Trading / レバレッジ取引

    /// <summary>Bitcoin/Japanese Yen pair for leveraged trading</summary>
    /// <summary xml:lang="ja">ビットコイン/日本円 レバレッジ取引ペア</summary>
    BtcJpy,
    /// <summary>Ethereum/Japanese Yen pair for leveraged trading</summary>
    /// <summary xml:lang="ja">イーサリアム/日本円 レバレッジ取引ペア</summary>
    EthJpy,
    /// <summary>Bitcoin Cash/Japanese Yen pair for leveraged trading</summary>
    /// <summary xml:lang="ja">ビットコインキャッシュ/日本円 レバレッジ取引ペア</summary>
    BchJpy,
    /// <summary>Litecoin/Japanese Yen pair for leveraged trading</summary>
    /// <summary xml:lang="ja">ライトコイン/日本円 レバレッジ取引ペア</summary>
    LtcJpy,
    /// <summary>Ripple/Japanese Yen pair for leveraged trading</summary>
    /// <summary xml:lang="ja">リップル/日本円 レバレッジ取引ペア</summary>
    XrpJpy,
    /// <summary>Polkadot/Japanese Yen pair for leveraged trading</summary>
    /// <summary xml:lang="ja">ポルカドット/日本円 レバレッジ取引ペア</summary>
    DotJpy,
    /// <summary>Cosmos/Japanese Yen pair for leveraged trading</summary>
    /// <summary xml:lang="ja">コスモス/日本円 レバレッジ取引ペア</summary>
    AtomJpy,
    /// <summary>Cardano/Japanese Yen pair for leveraged trading</summary>
    /// <summary xml:lang="ja">カルダノ/日本円 レバレッジ取引ペア</summary>
    AdaJpy,
    /// <summary>Chainlink/Japanese Yen pair for leveraged trading</summary>
    /// <summary xml:lang="ja">チェーンリンク/日本円 レバレッジ取引ペア</summary>
    LinkJpy,
    /// <summary>Dogecoin/Japanese Yen pair for leveraged trading</summary>
    /// <summary xml:lang="ja">ドージコイン/日本円 レバレッジ取引ペア</summary>
    DogeJpy,
    /// <summary>Solana/Japanese Yen pair for leveraged trading</summary>
    /// <summary xml:lang="ja">ソラナ/日本円 レバレッジ取引ペア</summary>
    SolJpy,
    /// <summary>Astar/Japanese Yen pair for leveraged trading</summary>
    /// <summary xml:lang="ja">アスター/日本円 レバレッジ取引ペア</summary>
    AstrJpy
    
    #endregion
}